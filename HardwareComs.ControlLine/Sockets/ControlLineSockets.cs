using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.Dto;
using ControlLine.Exception;

namespace ControlLine.Sockets
{
    //TODO: refactor out hard coded data types
    public class ControlLineSockets : IControlLine
    {
        public const int MaxPayloadLength = 8;

        private ISocketClient _socketClient;
        private IControlLineStatusValidator _statusValidator;
        private IThreadOperations _threadOperations;

        public ControlLineSockets(ISocketClient socketClient, IControlLineStatusValidator statusValidator,
            IThreadOperations threadOperations)
        {
            _socketClient = socketClient;
            _statusValidator = statusValidator;
            _threadOperations = threadOperations;
        }

        public OperationResponseDto SendOperation(OperationDto operationDto)
        {
            var paramBytes = new List<byte>();
            foreach (var param in operationDto.Params)
            {
                try
                {
                    paramBytes.Add(GetDataType(param));
                    paramBytes.AddRange(ValueToBytes(param));
                }
                catch (ArgumentException)
                {
                    //TODO: handle
                }
            }

            var payload = new List<byte> {operationDto.Operation, operationDto.Device}.Concat(paramBytes).ToArray();
            try
            {
                _socketClient.Connect();
                _socketClient.Send(payload);
                try
                {
                    var response =
                        _threadOperations.WaitUntilFuncTimeout(() => _socketClient.Recieve(), operationDto.Timeout);
                    if (_statusValidator.IsError(response[0]))
                    {
                        throw _statusValidator.ValidateError(response[0]);
                    }
                    else
                    {
                        try
                        {
                            return new OperationResponseDto
                            {
                                Status = response[0],
                                Returns = BytesToValue(response.Skip(1).ToArray())
                            };
                        }
                        catch (ArgumentException)
                        {
                            return new OperationResponseDto
                            {
                                Status = response[0]
                            };
                        }
                    }
                }
                catch (ThreadTimeout)
                {
                    throw new ControlLineTimeOut();
                }
            }
            catch (SocketException)
            {
                throw new ControlLineOffline();
            }
            finally
            {
                _socketClient.Close();
            }
        }

        private IEnumerable<byte> ValueToBytes(int value)
        {
            var bytes = BitConverter.GetBytes(value);

            return GetDataType(value) switch
            {
                1 => bytes.Take(1).ToArray(),
                2 => bytes.Take(2).ToArray(),
                _ => throw new ArgumentException("value must be 8/16 bits")
            };
        }

        private int BytesToValue(IReadOnlyList<byte> bytes)
        {
            return bytes[0] switch
            {
                1 => bytes[1],
                2 => BitConverter.ToUInt16(bytes.Skip(1).Take(2).ToArray()),
                _ => throw new ArgumentException("invalid param length, must be 8/16 bits")
            };
        }

        private byte GetDataType(int value)
        {
            return (value >= 0) switch
            {
                true when value <= 255 => 1,
                true when value <= 65535 => 2,
                _ => throw new ArgumentException("value must be 8/16 bits")
            };
        }
    }
}