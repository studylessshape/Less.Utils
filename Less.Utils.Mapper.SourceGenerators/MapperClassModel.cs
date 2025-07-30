using Microsoft.CodeAnalysis;

namespace Less.Utils.Mapper.SourceGenerators
{
    internal class MapperClassModel
    {
        public MapperClassModel(INamedTypeSymbol fromType, INamedTypeSymbol toType)
        {
            FromType = fromType;
            ToType = toType;
        }

        public INamedTypeSymbol FromType { get; }
        public string FromTypeNamespace => FromType.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted));
        public INamedTypeSymbol ToType { get; }
        public string ToTypeNamespace => FromType.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted));
    }
}
