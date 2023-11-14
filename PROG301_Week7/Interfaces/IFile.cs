using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.Interfaces
{
    public interface IFile
    {
        string? Name { get; }
        string? Extension { get; }
        string? Contents { get; }
    }
}
