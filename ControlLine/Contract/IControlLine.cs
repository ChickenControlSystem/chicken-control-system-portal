﻿using ControlLine.Dto;

namespace ControlLine.Contract
{
    /// <summary>
    /// performs the actions to send data over the control line
    /// </summary>
    public interface IControlLine
    {
        /// <summary>
        /// sends an operation dto over to the control
        /// </summary>
        string SendOperation(OperationDto operationDto);
    }
}