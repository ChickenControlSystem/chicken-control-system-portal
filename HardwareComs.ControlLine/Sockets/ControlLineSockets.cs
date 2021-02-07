using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using CodeContracts;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.Dto;

namespace ControlLine.Sockets
{
    //TODO: refactor out hard coded data types
    public class ControlLineSockets : IControlLine
    {
        public const int MaxPayloadLength = 8;

        private readonly ISocketClient _socketClient;
        private readonly IControlLineStatusValidator _statusValidator;
        private readonly IThreadOperations _threadOperations;

        public ControlLineSockets(ISocketClient socketClient, IControlLineStatusValidator statusValidator,
            IThreadOperations threadOperations)
        {
            _socketClient = socketClient;
            _statusValidator = statusValidator;
            _threadOperations = threadOperations;
        }

        /// <exception cref="ArgumentException"></exception>>
        /// <exception cref="SocketException"></exception>>
        public OperationResponseDto SendOperation(OperationDto operationDto)
        {
            operationDto.Params.ToList()
                .ForEach(x => CodeContract.PreCondition<ArgumentException>(x > 0 && x <= 65535));

            var paramBytes = new List<byte>();
            foreach (var param in operationDto.Params)
            {
                paramBytes.Add(GetDataType(param));
                paramBytes.AddRange(ValueToBytes(param));
            }

            var payload = new List<byte> {operationDto.Operation, operationDto.Device}.Concat(paramBytes).ToArray();
            try
            {
                _socketClient.Connect();
                _socketClient.Send(payload);
                var response = _socketClient.Recieve();
                if (_statusValidator.IsError(response[0]))
                {
                    throw _statusValidator.ValidateError(response[0]);
                }
                else
                {
                    return new OperationResponseDto
                    {
                        Status = response[0],
                        Returns = BytesToValue(response.Skip(1).ToArray())
                    };
                }
            }
            finally
            {
                _socketClient.Close();
            }
        }

        private byte[] ValueToBytes(int value)
        {
            var bytes = BitConverter.GetBytes(value);

            return GetDataType(value) switch
            {
                1 => bytes.Take(1).ToArray(),
                2 => bytes.Take(2).ToArray(),
                _ => new byte[] { }
            };
        }

        private int BytesToValue(IReadOnlyList<byte> bytes)
        {
            return bytes[0] switch
            {
                1 => bytes[1],
                2 => BitConverter.ToUInt16(bytes.Skip(1).Take(2).ToArray()),
                _ => 0
            };
        }

        private byte GetDataType(int value)
        {
            return (value >= 0) switch
            {
                true when value <= 255 => 1,
                true when value <= 65535 => 2,
                _ => 0
            };
        }
    }
}