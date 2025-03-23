using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Contracts
{
    public interface IPasswordRequirementsConfig
    {
        int MinimumLength { get; }
        bool RequireUppercase { get; }
        bool RequireLowercase { get; }
        bool RequireDigit { get; }
        bool RequireSpecialCharacter { get; }
    }
}
