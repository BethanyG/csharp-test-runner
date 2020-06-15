using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.TestRunner.CSharp
{
    internal static class TestsFileRewriter
    {
        internal static void Rewrite(Options options)
        {
            if (!File.Exists(options.TestsFilePath))
            {
                return;
            }

            var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(options.TestsFilePath));
            var rewrittenRoot = syntaxTree.GetRoot().UnskipTests().CaptureConsoleOutput();
            File.WriteAllText(options.TestsFilePath, rewrittenRoot.ToString());
        }

        private static SyntaxNode UnskipTests(this SyntaxNode node) =>
            new UnskipTestsSyntaxRewriter().Visit(node);

        private static SyntaxNode CaptureConsoleOutput(this SyntaxNode node) =>
            new CaptureConsoleOutputSyntaxRewriter().Visit(node);

        private class UnskipTestsSyntaxRewriter : CSharpSyntaxRewriter
        {
            public override SyntaxNode VisitAttribute(AttributeSyntax node)
            {
                if (node.Name.ToString() == "Fact")
                {
                    return base.VisitAttribute(node.WithArgumentList(null));
                }

                return base.VisitAttribute(node);
            }
        }

        private class CaptureConsoleOutputSyntaxRewriter : CSharpSyntaxRewriter
        {
            public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node) =>
                base.VisitClassDeclaration(
                    node.WithBaseList(BaseList(
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(
                                    QualifiedName(
                                        IdentifierName("System"),
                                        IdentifierName("IDisposable"))))))
                        .AddMembers(
                            FieldDeclaration(
                                VariableDeclaration(
                                        QualifiedName(
                                            QualifiedName(
                                                IdentifierName("Xunit"),
                                                IdentifierName("Abstractions")),
                                            IdentifierName("ITestOutputHelper")))
                                    .WithVariables(
                                        SingletonSeparatedList(
                                            VariableDeclarator(
                                                Identifier("_testOutput"))))),
                            FieldDeclaration(
                                VariableDeclaration(
                                        QualifiedName(
                                            QualifiedName(
                                                IdentifierName("System"),
                                                IdentifierName("IO")),
                                            IdentifierName("StringWriter")))
                                    .WithVariables(
                                        SingletonSeparatedList(
                                            VariableDeclarator(
                                                Identifier("_stringWriter"))))),
                            ConstructorDeclaration(
                                    node.Identifier)
                                .WithModifiers(
                                    TokenList(
                                        Token(SyntaxKind.PublicKeyword)))
                                .WithParameterList(
                                    ParameterList(
                                        SingletonSeparatedList(
                                            Parameter(
                                                    Identifier("testOutput"))
                                                .WithType(
                                                    QualifiedName(
                                                        QualifiedName(
                                                            IdentifierName("Xunit"),
                                                            IdentifierName("Abstractions")),
                                                        IdentifierName("ITestOutputHelper"))))))
                                .WithBody(
                                    Block(
                                        ExpressionStatement(
                                            AssignmentExpression(
                                                SyntaxKind.SimpleAssignmentExpression,
                                                IdentifierName("_testOutput"),
                                                IdentifierName("testOutput"))),
                                        ExpressionStatement(
                                            AssignmentExpression(
                                                SyntaxKind.SimpleAssignmentExpression,
                                                IdentifierName("_stringWriter"),
                                                ObjectCreationExpression(
                                                        QualifiedName(
                                                            QualifiedName(
                                                                IdentifierName("System"),
                                                                IdentifierName("IO")),
                                                            IdentifierName("StringWriter")))
                                                    .WithArgumentList(
                                                        ArgumentList()))),
                                        ExpressionStatement(
                                            InvocationExpression(
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("System"),
                                                            IdentifierName("Console")),
                                                        IdentifierName("SetOut")))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                IdentifierName("_stringWriter")))))),
                                        ExpressionStatement(
                                            InvocationExpression(
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("System"),
                                                            IdentifierName("Console")),
                                                        IdentifierName("SetError")))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                IdentifierName("_stringWriter")))))))
                                        .WithCloseBraceToken(
                                            Token(
                                                TriviaList(
                                                    new []{
                                                        Trivia(
                                                            IfDirectiveTrivia(
                                                                ParenthesizedExpression(
                                                                    BinaryExpression(
                                                                        SyntaxKind.LogicalOrExpression,
                                                                        IdentifierName("NETCOREAPP3_0"),
                                                                        IdentifierName("NETCOREAPP3_1"))),
                                                                true,
                                                                false,
                                                                false)),
                                                        DisabledText(@"        System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
"),
                                                        Trivia(
                                                            EndIfDirectiveTrivia(
                                                                true))}),
                                                SyntaxKind.CloseBraceToken,
                                                TriviaList()))),
                            MethodDeclaration(
                                    PredefinedType(
                                        Token(SyntaxKind.VoidKeyword)),
                                    Identifier("Dispose"))
                                .WithModifiers(
                                    TokenList(
                                        Token(SyntaxKind.PublicKeyword)))
                                .WithBody(
                                    Block(
                                        LocalDeclarationStatement(
                                            VariableDeclaration(
                                                    IdentifierName("var"))
                                                .WithVariables(
                                                    SingletonSeparatedList(
                                                        VariableDeclarator(
                                                                Identifier("output"))
                                                            .WithInitializer(
                                                                EqualsValueClause(
                                                                    InvocationExpression(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            IdentifierName("_stringWriter"),
                                                                            IdentifierName("ToString")))))))),
                                        IfStatement(
                                            BinaryExpression(
                                                SyntaxKind.GreaterThanExpression,
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName("output"),
                                                    IdentifierName("Length")),
                                                LiteralExpression(
                                                    SyntaxKind.NumericLiteralExpression,
                                                    Literal(500))),
                                            Block(
                                                SingletonList<StatementSyntax>(
                                                    ExpressionStatement(
                                                        AssignmentExpression(
                                                            SyntaxKind.SimpleAssignmentExpression,
                                                            IdentifierName("output"),
                                                            BinaryExpression(
                                                                SyntaxKind.AddExpression,
                                                                InvocationExpression(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            IdentifierName("output"),
                                                                            IdentifierName("Substring")))
                                                                    .WithArgumentList(
                                                                        ArgumentList(
                                                                            SeparatedList<ArgumentSyntax>(
                                                                                new SyntaxNodeOrToken[]
                                                                                {
                                                                                    Argument(
                                                                                        LiteralExpression(
                                                                                            SyntaxKind.NumericLiteralExpression,
                                                                                            Literal(0))),
                                                                                    Token(SyntaxKind.CommaToken),
                                                                                    Argument(
                                                                                        LiteralExpression(
                                                                                            SyntaxKind.NumericLiteralExpression,
                                                                                            Literal(450)))
                                                                                }))),
                                                                LiteralExpression(
                                                                    SyntaxKind.StringLiteralExpression,
                                                                    Literal(@"

Output was truncated. Please limit to 500 chars.")))))))),
                                        ExpressionStatement(
                                            InvocationExpression(
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName("_testOutput"),
                                                        IdentifierName("WriteLine")))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                IdentifierName("output"))))))))).NormalizeWhitespace());
        }
    }
}