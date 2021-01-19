using System.Collections.Generic;

namespace ControlLine.Dto
{
    public class OperationDto
    {
        public byte Operation { get; set; }
        
        public byte Device { get; set; }
        
        public int[] Params { get; set; }
    }
}