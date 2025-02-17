using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Compiler_MyOwnLanguage_.CodeAnalysis
{
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
