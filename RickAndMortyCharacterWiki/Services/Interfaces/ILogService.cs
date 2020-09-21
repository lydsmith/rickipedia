using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RickAndMortyCharacterWiki.Services.Interfaces
{
    public interface ILogService
    {
        void Initialize(Assembly assembly, string assemblyName);
    }
}
