using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Dto;
using ControlLine.Exception;

namespace ControlLine.Sockets
{
    //TODO: refactor out hard coded data types
    public class ControlLineSockets : IControlLine
    {

        private IRawSocketClient _socketClient;
        private IControlLineStatusValidator _statusValidator;

        public ControlLineSockets(IRawSocketClient socketClient, IControlLineStatusValidator statusValidator)
        {
            _socketClient = socketClient;
            _statusValidator = statusValidator;
        }
        
        public OperationResponseDto SendOperation(OperationDto operationDto)
        {
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
                var task = Task.Run(() => _socketClient.Recieve());
                if (task.Wait(5000))
                {
                    var response = task.Result;
                    if (_statusValidator.IsError(response[0]))
                    {
                        throw _statusValidator.ValidateError(response[0]);
                    }
                    else
                    {
                        return new OperationResponseDto()
                        {
                            Status = response[0],
                            Returns = BytesToValue(response.Skip(1).ToArray())
                        };
                    }
                }
                else
                {
                    throw new ControlLineTimeOut();
                }
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null && e.InnerException.GetType() == typeof(SocketException))
                {
                    throw new ControlLineOffline();
                }
                else if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (SocketException e)
            {
                throw new ControlLineOffline();
            }
            catch (System.Exception e)
            {
                throw e;
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
                0 => bytes.Take(1).ToArray(),
                1 => bytes.Take(2).ToArray(),
                _ => throw new ArgumentException("value must be 8/16 bits")
            };
        }
        
        private int BytesToValue(IReadOnlyList<byte> bytes)
        {
            return bytes[0] switch
            {
                0 => bytes[1],
                1 => BitConverter.ToUInt16(bytes.Skip(1).Take(2).ToArray()),
                _ => throw new ArgumentException("invalid param length, must be 8/16 bits")
            };
        }

        private byte GetDataType(int value)
        {
            return (value >= 0) switch
            {
                true when value <= 255 => 0,
                true when value <= 65535 => 1,
                _ => throw new ArgumentException("value must be 8/16 bits")
            };
        }
    }
}
