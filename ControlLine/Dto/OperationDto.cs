using System.Collections.Generic;

namespace ControlLine.Dto
{
    public class OperationDto
    {
        public byte Name { get; set; }
        
        public byte Device { get; set; }
        
        public byte[] Params { get; set; }
    }
}