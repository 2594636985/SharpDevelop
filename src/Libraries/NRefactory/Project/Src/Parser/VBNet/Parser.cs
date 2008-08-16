
#line  1 "VBNET.ATG" 
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.Parser.VB;
using ASTAttribute = ICSharpCode.NRefactory.Ast.Attribute;
/*
  Parser.frame file for NRefactory.
 */
using System;
using System.Reflection;

namespace ICSharpCode.NRefactory.Parser.VB {



partial class Parser : AbstractParser
{
	const int maxT = 207;

	const  bool   T            = true;
	const  bool   x            = false;
	

#line  12 "VBNET.ATG" 


/*

*/

	void VBNET() {

#line  232 "VBNET.ATG" 
		lexer.NextToken(); // get the first token
		compilationUnit = new CompilationUnit();
		
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (la.kind == 137) {
			OptionStmt();
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		while (la.kind == 109) {
			ImportsStmt();
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		while (
#line  239 "VBNET.ATG" 
IsGlobalAttrTarget()) {
			GlobalAttributeSection();
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		while (StartOf(1)) {
			NamespaceMemberDecl();
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		Expect(0);
	}

	void EndOfStmt() {
		if (la.kind == 1) {
			lexer.NextToken();
		} else if (la.kind == 13) {
			lexer.NextToken();
		} else SynErr(208);
	}

	void OptionStmt() {

#line  244 "VBNET.ATG" 
		INode node = null; bool val = true; 
		Expect(137);

#line  245 "VBNET.ATG" 
		Location startPos = t.Location; 
		if (la.kind == 96) {
			lexer.NextToken();
			if (la.kind == 135 || la.kind == 136) {
				OptionValue(
#line  247 "VBNET.ATG" 
ref val);
			}

#line  248 "VBNET.ATG" 
			node = new OptionDeclaration(OptionType.Explicit, val); 
		} else if (la.kind == 165) {
			lexer.NextToken();
			if (la.kind == 135 || la.kind == 136) {
				OptionValue(
#line  250 "VBNET.ATG" 
ref val);
			}

#line  251 "VBNET.ATG" 
			node = new OptionDeclaration(OptionType.Strict, val); 
		} else if (la.kind == 71) {
			lexer.NextToken();
			if (la.kind == 52) {
				lexer.NextToken();

#line  253 "VBNET.ATG" 
				node = new OptionDeclaration(OptionType.CompareBinary, val); 
			} else if (la.kind == 170) {
				lexer.NextToken();

#line  254 "VBNET.ATG" 
				node = new OptionDeclaration(OptionType.CompareText, val); 
			} else SynErr(209);
		} else SynErr(210);
		EndOfStmt();

#line  259 "VBNET.ATG" 
		if (node != null) {
		node.StartLocation = startPos;
		node.EndLocation   = t.Location;
		compilationUnit.AddChild(node);
		}
		
	}

	void ImportsStmt() {

#line  282 "VBNET.ATG" 
		List<Using> usings = new List<Using>();
		
		Expect(109);

#line  286 "VBNET.ATG" 
		Location startPos = t.Location;
		Using u;
		
		ImportClause(
#line  289 "VBNET.ATG" 
out u);

#line  289 "VBNET.ATG" 
		if (u != null) { usings.Add(u); } 
		while (la.kind == 12) {
			lexer.NextToken();
			ImportClause(
#line  291 "VBNET.ATG" 
out u);

#line  291 "VBNET.ATG" 
			if (u != null) { usings.Add(u); } 
		}
		EndOfStmt();

#line  295 "VBNET.ATG" 
		UsingDeclaration usingDeclaration = new UsingDeclaration(usings);
		usingDeclaration.StartLocation = startPos;
		usingDeclaration.EndLocation   = t.Location;
		compilationUnit.AddChild(usingDeclaration);
		
	}

	void GlobalAttributeSection() {
		Expect(28);

#line  2194 "VBNET.ATG" 
		Location startPos = t.Location; 
		if (la.kind == 50) {
			lexer.NextToken();
		} else if (la.kind == 122) {
			lexer.NextToken();
		} else SynErr(211);

#line  2196 "VBNET.ATG" 
		string attributeTarget = t.val != null ? t.val.ToLower(System.Globalization.CultureInfo.InvariantCulture) : null;
		List<ASTAttribute> attributes = new List<ASTAttribute>();
		ASTAttribute attribute;
		
		Expect(13);
		Attribute(
#line  2200 "VBNET.ATG" 
out attribute);

#line  2200 "VBNET.ATG" 
		attributes.Add(attribute); 
		while (
#line  2201 "VBNET.ATG" 
NotFinalComma()) {
			if (la.kind == 12) {
				lexer.NextToken();
				if (la.kind == 50) {
					lexer.NextToken();
				} else if (la.kind == 122) {
					lexer.NextToken();
				} else SynErr(212);
				Expect(13);
			}
			Attribute(
#line  2201 "VBNET.ATG" 
out attribute);

#line  2201 "VBNET.ATG" 
			attributes.Add(attribute); 
		}
		if (la.kind == 12) {
			lexer.NextToken();
		}
		Expect(27);
		EndOfStmt();

#line  2206 "VBNET.ATG" 
		AttributeSection section = new AttributeSection {
		AttributeTarget = attributeTarget,
		Attributes = attributes,
		StartLocation = startPos,
		EndLocation = t.EndLocation
		};
		compilationUnit.AddChild(section);
		
	}

	void NamespaceMemberDecl() {

#line  324 "VBNET.ATG" 
		ModifierList m = new ModifierList();
		AttributeSection section;
		List<AttributeSection> attributes = new List<AttributeSection>();
		string qualident;
		
		if (la.kind == 127) {
			lexer.NextToken();

#line  331 "VBNET.ATG" 
			Location startPos = t.Location;
			
			Qualident(
#line  333 "VBNET.ATG" 
out qualident);

#line  335 "VBNET.ATG" 
			INode node =  new NamespaceDeclaration(qualident);
			node.StartLocation = startPos;
			compilationUnit.AddChild(node);
			compilationUnit.BlockStart(node);
			
			EndOfStmt();
			NamespaceBody();

#line  343 "VBNET.ATG" 
			node.EndLocation = t.Location;
			compilationUnit.BlockEnd();
			
		} else if (StartOf(2)) {
			while (la.kind == 28) {
				AttributeSection(
#line  347 "VBNET.ATG" 
out section);

#line  347 "VBNET.ATG" 
				attributes.Add(section); 
			}
			while (StartOf(3)) {
				TypeModifier(
#line  348 "VBNET.ATG" 
m);
			}
			NonModuleDeclaration(
#line  348 "VBNET.ATG" 
m, attributes);
		} else SynErr(213);
	}

	void OptionValue(
#line  267 "VBNET.ATG" 
ref bool val) {
		if (la.kind == 136) {
			lexer.NextToken();

#line  269 "VBNET.ATG" 
			val = true; 
		} else if (la.kind == 135) {
			lexer.NextToken();

#line  271 "VBNET.ATG" 
			val = false; 
		} else SynErr(214);
	}

	void ImportClause(
#line  302 "VBNET.ATG" 
out Using u) {

#line  304 "VBNET.ATG" 
		string qualident  = null;
		TypeReference aliasedType = null;
		u = null;
		
		Qualident(
#line  308 "VBNET.ATG" 
out qualident);
		if (la.kind == 11) {
			lexer.NextToken();
			TypeName(
#line  309 "VBNET.ATG" 
out aliasedType);
		}

#line  311 "VBNET.ATG" 
		if (qualident != null && qualident.Length > 0) {
		if (aliasedType != null) {
			u = new Using(qualident, aliasedType);
		} else {
			u = new Using(qualident);
		}
		}
		
	}

	void Qualident(
#line  2947 "VBNET.ATG" 
out string qualident) {

#line  2949 "VBNET.ATG" 
		string name;
		qualidentBuilder.Length = 0; 
		
		Identifier();

#line  2953 "VBNET.ATG" 
		qualidentBuilder.Append(t.val); 
		while (
#line  2954 "VBNET.ATG" 
DotAndIdentOrKw()) {
			Expect(10);
			IdentifierOrKeyword(
#line  2954 "VBNET.ATG" 
out name);

#line  2954 "VBNET.ATG" 
			qualidentBuilder.Append('.'); qualidentBuilder.Append(name); 
		}

#line  2956 "VBNET.ATG" 
		qualident = qualidentBuilder.ToString(); 
	}

	void TypeName(
#line  2067 "VBNET.ATG" 
out TypeReference typeref) {

#line  2068 "VBNET.ATG" 
		ArrayList rank = null; 
		NonArrayTypeName(
#line  2070 "VBNET.ATG" 
out typeref, false);
		ArrayTypeModifiers(
#line  2074 "VBNET.ATG" 
out rank);

#line  2075 "VBNET.ATG" 
		if (rank != null && typeref != null) {
		typeref.RankSpecifier = (int[])rank.ToArray(typeof(int));
		}
		
	}

	void NamespaceBody() {
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (StartOf(1)) {
			NamespaceMemberDecl();
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		Expect(89);
		Expect(127);
		EndOfStmt();
	}

	void AttributeSection(
#line  2271 "VBNET.ATG" 
out AttributeSection section) {

#line  2273 "VBNET.ATG" 
		string attributeTarget = "";List<ASTAttribute> attributes = new List<ASTAttribute>();
		ASTAttribute attribute;
		
		
		Expect(28);

#line  2277 "VBNET.ATG" 
		Location startPos = t.Location; 
		if (
#line  2278 "VBNET.ATG" 
IsLocalAttrTarget()) {
			if (la.kind == 94) {
				lexer.NextToken();

#line  2279 "VBNET.ATG" 
				attributeTarget = "event";
			} else if (la.kind == 155) {
				lexer.NextToken();

#line  2280 "VBNET.ATG" 
				attributeTarget = "return";
			} else {
				Identifier();

#line  2283 "VBNET.ATG" 
				string val = t.val.ToLower(System.Globalization.CultureInfo.InvariantCulture);
				if (val != "field"	|| val != "method" ||
					val != "module" || val != "param"  ||
					val != "property" || val != "type")
				Error("attribute target specifier (event, return, field," +
						"method, module, param, property, or type) expected");
				attributeTarget = t.val;
				
			}
			Expect(13);
		}
		Attribute(
#line  2293 "VBNET.ATG" 
out attribute);

#line  2293 "VBNET.ATG" 
		attributes.Add(attribute); 
		while (
#line  2294 "VBNET.ATG" 
NotFinalComma()) {
			Expect(12);
			Attribute(
#line  2294 "VBNET.ATG" 
out attribute);

#line  2294 "VBNET.ATG" 
			attributes.Add(attribute); 
		}
		if (la.kind == 12) {
			lexer.NextToken();
		}
		Expect(27);

#line  2298 "VBNET.ATG" 
		section = new AttributeSection {
		AttributeTarget = attributeTarget,
		Attributes = attributes,
		StartLocation = startPos,
		EndLocation = t.EndLocation
		};
		
	}

	void TypeModifier(
#line  3027 "VBNET.ATG" 
ModifierList m) {
		switch (la.kind) {
		case 149: {
			lexer.NextToken();

#line  3028 "VBNET.ATG" 
			m.Add(Modifiers.Public, t.Location); 
			break;
		}
		case 148: {
			lexer.NextToken();

#line  3029 "VBNET.ATG" 
			m.Add(Modifiers.Protected, t.Location); 
			break;
		}
		case 100: {
			lexer.NextToken();

#line  3030 "VBNET.ATG" 
			m.Add(Modifiers.Internal, t.Location); 
			break;
		}
		case 146: {
			lexer.NextToken();

#line  3031 "VBNET.ATG" 
			m.Add(Modifiers.Private, t.Location); 
			break;
		}
		case 159: {
			lexer.NextToken();

#line  3032 "VBNET.ATG" 
			m.Add(Modifiers.Static, t.Location); 
			break;
		}
		case 158: {
			lexer.NextToken();

#line  3033 "VBNET.ATG" 
			m.Add(Modifiers.New, t.Location); 
			break;
		}
		case 123: {
			lexer.NextToken();

#line  3034 "VBNET.ATG" 
			m.Add(Modifiers.Abstract, t.Location); 
			break;
		}
		case 132: {
			lexer.NextToken();

#line  3035 "VBNET.ATG" 
			m.Add(Modifiers.Sealed, t.Location); 
			break;
		}
		case 205: {
			lexer.NextToken();

#line  3036 "VBNET.ATG" 
			m.Add(Modifiers.Partial, t.Location); 
			break;
		}
		default: SynErr(215); break;
		}
	}

	void NonModuleDeclaration(
#line  407 "VBNET.ATG" 
ModifierList m, List<AttributeSection> attributes) {

#line  409 "VBNET.ATG" 
		TypeReference typeRef = null;
		List<TypeReference> baseInterfaces = null;
		
		switch (la.kind) {
		case 68: {

#line  412 "VBNET.ATG" 
			m.Check(Modifiers.Classes); 
			lexer.NextToken();

#line  415 "VBNET.ATG" 
			TypeDeclaration newType = new TypeDeclaration(m.Modifier, attributes);
			newType.StartLocation = t.Location;
			compilationUnit.AddChild(newType);
			compilationUnit.BlockStart(newType);
			
			newType.Type       = ClassType.Class;
			
			Identifier();

#line  422 "VBNET.ATG" 
			newType.Name = t.val; 
			TypeParameterList(
#line  423 "VBNET.ATG" 
newType.Templates);
			EndOfStmt();

#line  425 "VBNET.ATG" 
			newType.BodyStartLocation = t.Location; 
			if (la.kind == 111) {
				ClassBaseType(
#line  426 "VBNET.ATG" 
out typeRef);

#line  426 "VBNET.ATG" 
				newType.BaseTypes.Add(typeRef); 
			}
			while (la.kind == 108) {
				TypeImplementsClause(
#line  427 "VBNET.ATG" 
out baseInterfaces);

#line  427 "VBNET.ATG" 
				newType.BaseTypes.AddRange(baseInterfaces); 
			}
			ClassBody(
#line  428 "VBNET.ATG" 
newType);
			Expect(89);
			Expect(68);

#line  429 "VBNET.ATG" 
			newType.EndLocation = t.EndLocation; 
			EndOfStmt();

#line  432 "VBNET.ATG" 
			compilationUnit.BlockEnd();
			
			break;
		}
		case 122: {
			lexer.NextToken();

#line  436 "VBNET.ATG" 
			m.Check(Modifiers.VBModules);
			TypeDeclaration newType = new TypeDeclaration(m.Modifier, attributes);
			compilationUnit.AddChild(newType);
			compilationUnit.BlockStart(newType);
			newType.StartLocation = m.GetDeclarationLocation(t.Location);
			newType.Type = ClassType.Module;
			
			Identifier();

#line  443 "VBNET.ATG" 
			newType.Name = t.val; 
			EndOfStmt();

#line  445 "VBNET.ATG" 
			newType.BodyStartLocation = t.Location; 
			ModuleBody(
#line  446 "VBNET.ATG" 
newType);

#line  448 "VBNET.ATG" 
			compilationUnit.BlockEnd();
			
			break;
		}
		case 167: {
			lexer.NextToken();

#line  452 "VBNET.ATG" 
			m.Check(Modifiers.VBStructures);
			TypeDeclaration newType = new TypeDeclaration(m.Modifier, attributes);
			compilationUnit.AddChild(newType);
			compilationUnit.BlockStart(newType);
			newType.StartLocation = m.GetDeclarationLocation(t.Location);
			newType.Type = ClassType.Struct;
			
			Identifier();

#line  459 "VBNET.ATG" 
			newType.Name = t.val; 
			TypeParameterList(
#line  460 "VBNET.ATG" 
newType.Templates);
			EndOfStmt();

#line  462 "VBNET.ATG" 
			newType.BodyStartLocation = t.Location; 
			while (la.kind == 108) {
				TypeImplementsClause(
#line  463 "VBNET.ATG" 
out baseInterfaces);

#line  463 "VBNET.ATG" 
				newType.BaseTypes.AddRange(baseInterfaces);
			}
			StructureBody(
#line  464 "VBNET.ATG" 
newType);

#line  466 "VBNET.ATG" 
			compilationUnit.BlockEnd();
			
			break;
		}
		case 91: {
			lexer.NextToken();

#line  471 "VBNET.ATG" 
			m.Check(Modifiers.VBEnums);
			TypeDeclaration newType = new TypeDeclaration(m.Modifier, attributes);
			newType.StartLocation = m.GetDeclarationLocation(t.Location);
			compilationUnit.AddChild(newType);
			compilationUnit.BlockStart(newType);
			
			newType.Type = ClassType.Enum;
			
			Identifier();

#line  479 "VBNET.ATG" 
			newType.Name = t.val; 
			if (la.kind == 49) {
				lexer.NextToken();
				NonArrayTypeName(
#line  480 "VBNET.ATG" 
out typeRef, false);

#line  480 "VBNET.ATG" 
				newType.BaseTypes.Add(typeRef); 
			}
			EndOfStmt();

#line  482 "VBNET.ATG" 
			newType.BodyStartLocation = t.Location; 
			EnumBody(
#line  483 "VBNET.ATG" 
newType);

#line  485 "VBNET.ATG" 
			compilationUnit.BlockEnd();
			
			break;
		}
		case 113: {
			lexer.NextToken();

#line  490 "VBNET.ATG" 
			m.Check(Modifiers.VBInterfacs);
			TypeDeclaration newType = new TypeDeclaration(m.Modifier, attributes);
			newType.StartLocation = m.GetDeclarationLocation(t.Location);
			compilationUnit.AddChild(newType);
			compilationUnit.BlockStart(newType);
			newType.Type = ClassType.Interface;
			
			Identifier();

#line  497 "VBNET.ATG" 
			newType.Name = t.val; 
			TypeParameterList(
#line  498 "VBNET.ATG" 
newType.Templates);
			EndOfStmt();

#line  500 "VBNET.ATG" 
			newType.BodyStartLocation = t.Location; 
			while (la.kind == 111) {
				InterfaceBase(
#line  501 "VBNET.ATG" 
out baseInterfaces);

#line  501 "VBNET.ATG" 
				newType.BaseTypes.AddRange(baseInterfaces); 
			}
			InterfaceBody(
#line  502 "VBNET.ATG" 
newType);

#line  504 "VBNET.ATG" 
			compilationUnit.BlockEnd();
			
			break;
		}
		case 81: {
			lexer.NextToken();

#line  509 "VBNET.ATG" 
			m.Check(Modifiers.VBDelegates);
			DelegateDeclaration delegateDeclr = new DelegateDeclaration(m.Modifier, attributes);
			delegateDeclr.ReturnType = new TypeReference("", "System.Void");
			delegateDeclr.StartLocation = m.GetDeclarationLocation(t.Location);
			List<ParameterDeclarationExpression> p = new List<ParameterDeclarationExpression>();
			
			if (la.kind == 168) {
				lexer.NextToken();
				Identifier();

#line  516 "VBNET.ATG" 
				delegateDeclr.Name = t.val; 
				TypeParameterList(
#line  517 "VBNET.ATG" 
delegateDeclr.Templates);
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  518 "VBNET.ATG" 
p);
					}
					Expect(26);

#line  518 "VBNET.ATG" 
					delegateDeclr.Parameters = p; 
				}
			} else if (la.kind == 101) {
				lexer.NextToken();
				Identifier();

#line  520 "VBNET.ATG" 
				delegateDeclr.Name = t.val; 
				TypeParameterList(
#line  521 "VBNET.ATG" 
delegateDeclr.Templates);
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  522 "VBNET.ATG" 
p);
					}
					Expect(26);

#line  522 "VBNET.ATG" 
					delegateDeclr.Parameters = p; 
				}
				if (la.kind == 49) {
					lexer.NextToken();

#line  523 "VBNET.ATG" 
					TypeReference type; 
					TypeName(
#line  523 "VBNET.ATG" 
out type);

#line  523 "VBNET.ATG" 
					delegateDeclr.ReturnType = type; 
				}
			} else SynErr(216);

#line  525 "VBNET.ATG" 
			delegateDeclr.EndLocation = t.EndLocation; 
			EndOfStmt();

#line  528 "VBNET.ATG" 
			compilationUnit.AddChild(delegateDeclr);
			
			break;
		}
		default: SynErr(217); break;
		}
	}

	void TypeParameterList(
#line  352 "VBNET.ATG" 
List<TemplateDefinition> templates) {

#line  354 "VBNET.ATG" 
		TemplateDefinition template;
		
		if (
#line  357 "VBNET.ATG" 
la.kind == Tokens.OpenParenthesis && Peek(1).kind == Tokens.Of) {
			lexer.NextToken();
			Expect(202);
			TypeParameter(
#line  358 "VBNET.ATG" 
out template);

#line  360 "VBNET.ATG" 
			if (template != null) templates.Add(template);
			
			while (la.kind == 12) {
				lexer.NextToken();
				TypeParameter(
#line  363 "VBNET.ATG" 
out template);

#line  365 "VBNET.ATG" 
				if (template != null) templates.Add(template);
				
			}
			Expect(26);
		}
	}

	void TypeParameter(
#line  373 "VBNET.ATG" 
out TemplateDefinition template) {
		Identifier();

#line  375 "VBNET.ATG" 
		template = new TemplateDefinition(t.val, null); 
		if (la.kind == 49) {
			TypeParameterConstraints(
#line  376 "VBNET.ATG" 
template);
		}
	}

	void Identifier() {
		switch (la.kind) {
		case 2: {
			lexer.NextToken();
			break;
		}
		case 170: {
			lexer.NextToken();
			break;
		}
		case 52: {
			lexer.NextToken();
			break;
		}
		case 71: {
			lexer.NextToken();
			break;
		}
		case 206: {
			lexer.NextToken();
			break;
		}
		case 50: {
			lexer.NextToken();
			break;
		}
		case 48: {
			lexer.NextToken();
			break;
		}
		case 51: {
			lexer.NextToken();
			break;
		}
		case 145: {
			lexer.NextToken();
			break;
		}
		case 177: {
			lexer.NextToken();
			break;
		}
		case 178: {
			lexer.NextToken();
			break;
		}
		case 135: {
			lexer.NextToken();
			break;
		}
		case 96: {
			lexer.NextToken();
			break;
		}
		default: SynErr(218); break;
		}
	}

	void TypeParameterConstraints(
#line  380 "VBNET.ATG" 
TemplateDefinition template) {

#line  382 "VBNET.ATG" 
		TypeReference constraint;
		
		Expect(49);
		if (la.kind == 23) {
			lexer.NextToken();
			TypeParameterConstraint(
#line  388 "VBNET.ATG" 
out constraint);

#line  388 "VBNET.ATG" 
			if (constraint != null) { template.Bases.Add(constraint); } 
			while (la.kind == 12) {
				lexer.NextToken();
				TypeParameterConstraint(
#line  391 "VBNET.ATG" 
out constraint);

#line  391 "VBNET.ATG" 
				if (constraint != null) { template.Bases.Add(constraint); } 
			}
			Expect(24);
		} else if (StartOf(5)) {
			TypeParameterConstraint(
#line  394 "VBNET.ATG" 
out constraint);

#line  394 "VBNET.ATG" 
			if (constraint != null) { template.Bases.Add(constraint); } 
		} else SynErr(219);
	}

	void TypeParameterConstraint(
#line  398 "VBNET.ATG" 
out TypeReference constraint) {

#line  399 "VBNET.ATG" 
		constraint = null; 
		if (la.kind == 68) {
			lexer.NextToken();

#line  400 "VBNET.ATG" 
			constraint = TypeReference.ClassConstraint; 
		} else if (la.kind == 167) {
			lexer.NextToken();

#line  401 "VBNET.ATG" 
			constraint = TypeReference.StructConstraint; 
		} else if (la.kind == 128) {
			lexer.NextToken();

#line  402 "VBNET.ATG" 
			constraint = TypeReference.NewConstraint; 
		} else if (StartOf(6)) {
			TypeName(
#line  403 "VBNET.ATG" 
out constraint);
		} else SynErr(220);
	}

	void ClassBaseType(
#line  749 "VBNET.ATG" 
out TypeReference typeRef) {

#line  751 "VBNET.ATG" 
		typeRef = null;
		
		Expect(111);
		TypeName(
#line  754 "VBNET.ATG" 
out typeRef);
		EndOfStmt();
	}

	void TypeImplementsClause(
#line  1549 "VBNET.ATG" 
out List<TypeReference> baseInterfaces) {

#line  1551 "VBNET.ATG" 
		baseInterfaces = new List<TypeReference>();
		TypeReference type = null;
		
		Expect(108);
		TypeName(
#line  1554 "VBNET.ATG" 
out type);

#line  1556 "VBNET.ATG" 
		baseInterfaces.Add(type);
		
		while (la.kind == 12) {
			lexer.NextToken();
			TypeName(
#line  1559 "VBNET.ATG" 
out type);

#line  1560 "VBNET.ATG" 
			baseInterfaces.Add(type); 
		}
		EndOfStmt();
	}

	void ClassBody(
#line  542 "VBNET.ATG" 
TypeDeclaration newType) {

#line  543 "VBNET.ATG" 
		AttributeSection section; 
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (StartOf(7)) {

#line  546 "VBNET.ATG" 
			List<AttributeSection> attributes = new List<AttributeSection>();
			ModifierList m = new ModifierList();
			
			while (la.kind == 28) {
				AttributeSection(
#line  549 "VBNET.ATG" 
out section);

#line  549 "VBNET.ATG" 
				attributes.Add(section); 
			}
			while (StartOf(8)) {
				MemberModifier(
#line  550 "VBNET.ATG" 
m);
			}
			ClassMemberDecl(
#line  551 "VBNET.ATG" 
m, attributes);
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
	}

	void ModuleBody(
#line  573 "VBNET.ATG" 
TypeDeclaration newType) {

#line  574 "VBNET.ATG" 
		AttributeSection section; 
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (StartOf(7)) {

#line  577 "VBNET.ATG" 
			List<AttributeSection> attributes = new List<AttributeSection>();
			ModifierList m = new ModifierList();
			
			while (la.kind == 28) {
				AttributeSection(
#line  580 "VBNET.ATG" 
out section);

#line  580 "VBNET.ATG" 
				attributes.Add(section); 
			}
			while (StartOf(8)) {
				MemberModifier(
#line  581 "VBNET.ATG" 
m);
			}
			ClassMemberDecl(
#line  582 "VBNET.ATG" 
m, attributes);
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		Expect(89);
		Expect(122);

#line  585 "VBNET.ATG" 
		newType.EndLocation = t.EndLocation; 
		EndOfStmt();
	}

	void StructureBody(
#line  556 "VBNET.ATG" 
TypeDeclaration newType) {

#line  557 "VBNET.ATG" 
		AttributeSection section; 
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (StartOf(7)) {

#line  560 "VBNET.ATG" 
			List<AttributeSection> attributes = new List<AttributeSection>();
			ModifierList m = new ModifierList();
			
			while (la.kind == 28) {
				AttributeSection(
#line  563 "VBNET.ATG" 
out section);

#line  563 "VBNET.ATG" 
				attributes.Add(section); 
			}
			while (StartOf(8)) {
				MemberModifier(
#line  564 "VBNET.ATG" 
m);
			}
			StructureMemberDecl(
#line  565 "VBNET.ATG" 
m, attributes);
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		Expect(89);
		Expect(167);

#line  568 "VBNET.ATG" 
		newType.EndLocation = t.EndLocation; 
		EndOfStmt();
	}

	void NonArrayTypeName(
#line  2093 "VBNET.ATG" 
out TypeReference typeref, bool canBeUnbound) {

#line  2095 "VBNET.ATG" 
		string name;
		typeref = null;
		bool isGlobal = false;
		
		if (StartOf(9)) {
			if (la.kind == 200) {
				lexer.NextToken();
				Expect(10);

#line  2100 "VBNET.ATG" 
				isGlobal = true; 
			}
			QualIdentAndTypeArguments(
#line  2101 "VBNET.ATG" 
out typeref, canBeUnbound);

#line  2102 "VBNET.ATG" 
			typeref.IsGlobal = isGlobal; 
			while (la.kind == 10) {
				lexer.NextToken();

#line  2103 "VBNET.ATG" 
				TypeReference nestedTypeRef; 
				QualIdentAndTypeArguments(
#line  2104 "VBNET.ATG" 
out nestedTypeRef, canBeUnbound);

#line  2105 "VBNET.ATG" 
				typeref = new InnerClassTypeReference(typeref, nestedTypeRef.Type, nestedTypeRef.GenericTypes); 
			}
		} else if (la.kind == 134) {
			lexer.NextToken();

#line  2108 "VBNET.ATG" 
			typeref = new TypeReference("System.Object"); 
			if (la.kind == 21) {
				lexer.NextToken();

#line  2112 "VBNET.ATG" 
				List<TypeReference> typeArguments = new List<TypeReference>(1);
				if (typeref != null) typeArguments.Add(typeref);
				typeref = new TypeReference("System.Nullable", typeArguments);
				
			}
		} else if (StartOf(10)) {
			PrimitiveTypeName(
#line  2118 "VBNET.ATG" 
out name);

#line  2118 "VBNET.ATG" 
			typeref = new TypeReference(name); 
			if (la.kind == 21) {
				lexer.NextToken();

#line  2122 "VBNET.ATG" 
				List<TypeReference> typeArguments = new List<TypeReference>(1);
				if (typeref != null) typeArguments.Add(typeref);
				typeref = new TypeReference("System.Nullable", typeArguments);
				
			}
		} else SynErr(221);
	}

	void EnumBody(
#line  589 "VBNET.ATG" 
TypeDeclaration newType) {

#line  590 "VBNET.ATG" 
		FieldDeclaration f; 
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (StartOf(11)) {
			EnumMemberDecl(
#line  593 "VBNET.ATG" 
out f);

#line  595 "VBNET.ATG" 
			SetParent(f.Fields, f);
			compilationUnit.AddChild(f);
			
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		Expect(89);
		Expect(91);

#line  600 "VBNET.ATG" 
		newType.EndLocation = t.EndLocation; 
		EndOfStmt();
	}

	void InterfaceBase(
#line  1534 "VBNET.ATG" 
out List<TypeReference> bases) {

#line  1536 "VBNET.ATG" 
		TypeReference type;
		bases = new List<TypeReference>();
		
		Expect(111);
		TypeName(
#line  1540 "VBNET.ATG" 
out type);

#line  1540 "VBNET.ATG" 
		bases.Add(type); 
		while (la.kind == 12) {
			lexer.NextToken();
			TypeName(
#line  1543 "VBNET.ATG" 
out type);

#line  1543 "VBNET.ATG" 
			bases.Add(type); 
		}
		EndOfStmt();
	}

	void InterfaceBody(
#line  604 "VBNET.ATG" 
TypeDeclaration newType) {
		while (la.kind == 1 || la.kind == 13) {
			EndOfStmt();
		}
		while (StartOf(12)) {
			InterfaceMemberDecl();
			while (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
			}
		}
		Expect(89);
		Expect(113);

#line  610 "VBNET.ATG" 
		newType.EndLocation = t.EndLocation; 
		EndOfStmt();
	}

	void FormalParameterList(
#line  2308 "VBNET.ATG" 
List<ParameterDeclarationExpression> parameter) {

#line  2309 "VBNET.ATG" 
		ParameterDeclarationExpression p; 
		FormalParameter(
#line  2311 "VBNET.ATG" 
out p);

#line  2311 "VBNET.ATG" 
		if (p != null) parameter.Add(p); 
		while (la.kind == 12) {
			lexer.NextToken();
			FormalParameter(
#line  2313 "VBNET.ATG" 
out p);

#line  2313 "VBNET.ATG" 
			if (p != null) parameter.Add(p); 
		}
	}

	void MemberModifier(
#line  3039 "VBNET.ATG" 
ModifierList m) {
		switch (la.kind) {
		case 123: {
			lexer.NextToken();

#line  3040 "VBNET.ATG" 
			m.Add(Modifiers.Abstract, t.Location);
			break;
		}
		case 80: {
			lexer.NextToken();

#line  3041 "VBNET.ATG" 
			m.Add(Modifiers.Default, t.Location);
			break;
		}
		case 100: {
			lexer.NextToken();

#line  3042 "VBNET.ATG" 
			m.Add(Modifiers.Internal, t.Location);
			break;
		}
		case 158: {
			lexer.NextToken();

#line  3043 "VBNET.ATG" 
			m.Add(Modifiers.New, t.Location);
			break;
		}
		case 143: {
			lexer.NextToken();

#line  3044 "VBNET.ATG" 
			m.Add(Modifiers.Override, t.Location);
			break;
		}
		case 124: {
			lexer.NextToken();

#line  3045 "VBNET.ATG" 
			m.Add(Modifiers.Abstract, t.Location);
			break;
		}
		case 146: {
			lexer.NextToken();

#line  3046 "VBNET.ATG" 
			m.Add(Modifiers.Private, t.Location);
			break;
		}
		case 148: {
			lexer.NextToken();

#line  3047 "VBNET.ATG" 
			m.Add(Modifiers.Protected, t.Location);
			break;
		}
		case 149: {
			lexer.NextToken();

#line  3048 "VBNET.ATG" 
			m.Add(Modifiers.Public, t.Location);
			break;
		}
		case 132: {
			lexer.NextToken();

#line  3049 "VBNET.ATG" 
			m.Add(Modifiers.Sealed, t.Location);
			break;
		}
		case 133: {
			lexer.NextToken();

#line  3050 "VBNET.ATG" 
			m.Add(Modifiers.Sealed, t.Location);
			break;
		}
		case 159: {
			lexer.NextToken();

#line  3051 "VBNET.ATG" 
			m.Add(Modifiers.Static, t.Location);
			break;
		}
		case 142: {
			lexer.NextToken();

#line  3052 "VBNET.ATG" 
			m.Add(Modifiers.Virtual, t.Location);
			break;
		}
		case 141: {
			lexer.NextToken();

#line  3053 "VBNET.ATG" 
			m.Add(Modifiers.Overloads, t.Location);
			break;
		}
		case 151: {
			lexer.NextToken();

#line  3054 "VBNET.ATG" 
			m.Add(Modifiers.ReadOnly, t.Location);
			break;
		}
		case 185: {
			lexer.NextToken();

#line  3055 "VBNET.ATG" 
			m.Add(Modifiers.WriteOnly, t.Location);
			break;
		}
		case 184: {
			lexer.NextToken();

#line  3056 "VBNET.ATG" 
			m.Add(Modifiers.WithEvents, t.Location);
			break;
		}
		case 82: {
			lexer.NextToken();

#line  3057 "VBNET.ATG" 
			m.Add(Modifiers.Dim, t.Location);
			break;
		}
		case 205: {
			lexer.NextToken();

#line  3058 "VBNET.ATG" 
			m.Add(Modifiers.Partial, t.Location);
			break;
		}
		default: SynErr(222); break;
		}
	}

	void ClassMemberDecl(
#line  745 "VBNET.ATG" 
ModifierList m, List<AttributeSection> attributes) {
		StructureMemberDecl(
#line  746 "VBNET.ATG" 
m, attributes);
	}

	void StructureMemberDecl(
#line  759 "VBNET.ATG" 
ModifierList m, List<AttributeSection> attributes) {

#line  761 "VBNET.ATG" 
		TypeReference type = null;
		List<ParameterDeclarationExpression> p = new List<ParameterDeclarationExpression>();
		Statement stmt = null;
		List<VariableDeclaration> variableDeclarators = new List<VariableDeclaration>();
		List<TemplateDefinition> templates = new List<TemplateDefinition>();
		
		switch (la.kind) {
		case 68: case 81: case 91: case 113: case 122: case 167: {
			NonModuleDeclaration(
#line  768 "VBNET.ATG" 
m, attributes);
			break;
		}
		case 168: {
			lexer.NextToken();

#line  772 "VBNET.ATG" 
			Location startPos = t.Location;
			
			if (StartOf(13)) {

#line  776 "VBNET.ATG" 
				string name = String.Empty;
				MethodDeclaration methodDeclaration; List<string> handlesClause = null;
				List<InterfaceImplementation> implementsClause = null;
				
				Identifier();

#line  782 "VBNET.ATG" 
				name = t.val;
				m.Check(Modifiers.VBMethods);
				
				TypeParameterList(
#line  785 "VBNET.ATG" 
templates);
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  786 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				if (la.kind == 106 || la.kind == 108) {
					if (la.kind == 108) {
						ImplementsClause(
#line  789 "VBNET.ATG" 
out implementsClause);
					} else {
						HandlesClause(
#line  791 "VBNET.ATG" 
out handlesClause);
					}
				}

#line  794 "VBNET.ATG" 
				Location endLocation = t.EndLocation; 
				if (
#line  797 "VBNET.ATG" 
IsMustOverride(m)) {
					EndOfStmt();

#line  800 "VBNET.ATG" 
					methodDeclaration = new MethodDeclaration {
					Name = name, Modifier = m.Modifier, Parameters = p, Attributes = attributes,
					StartLocation = m.GetDeclarationLocation(startPos), EndLocation = endLocation,
					TypeReference = new TypeReference("", "System.Void"),
					Templates = templates,
					HandlesClause = handlesClause,
					InterfaceImplementations = implementsClause
					};
					compilationUnit.AddChild(methodDeclaration);
					
				} else if (la.kind == 1) {
					lexer.NextToken();

#line  813 "VBNET.ATG" 
					methodDeclaration = new MethodDeclaration {
					Name = name, Modifier = m.Modifier, Parameters = p, Attributes = attributes,
					StartLocation = m.GetDeclarationLocation(startPos), EndLocation = endLocation,
					TypeReference = new TypeReference("", "System.Void"),
					Templates = templates,
					HandlesClause = handlesClause,
					InterfaceImplementations = implementsClause
					};
					compilationUnit.AddChild(methodDeclaration);
					

#line  824 "VBNET.ATG" 
					if (ParseMethodBodies) { 
					Block(
#line  825 "VBNET.ATG" 
out stmt);
					Expect(89);
					Expect(168);

#line  827 "VBNET.ATG" 
					} else {
					// don't parse method body
					lexer.SkipCurrentBlock(Tokens.Sub); stmt = new BlockStatement();
					  }
					

#line  833 "VBNET.ATG" 
					methodDeclaration.Body  = (BlockStatement)stmt; 

#line  834 "VBNET.ATG" 
					methodDeclaration.Body.EndLocation = t.EndLocation; 
					EndOfStmt();
				} else SynErr(223);
			} else if (la.kind == 128) {
				lexer.NextToken();
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  838 "VBNET.ATG" 
p);
					}
					Expect(26);
				}

#line  839 "VBNET.ATG" 
				m.Check(Modifiers.Constructors); 

#line  840 "VBNET.ATG" 
				Location constructorEndLocation = t.EndLocation; 
				Expect(1);

#line  843 "VBNET.ATG" 
				if (ParseMethodBodies) { 
				Block(
#line  844 "VBNET.ATG" 
out stmt);
				Expect(89);
				Expect(168);

#line  846 "VBNET.ATG" 
				} else {
				// don't parse method body
				lexer.SkipCurrentBlock(Tokens.Sub); stmt = new BlockStatement();
				  }
				

#line  852 "VBNET.ATG" 
				Location endLocation = t.EndLocation; 
				EndOfStmt();

#line  855 "VBNET.ATG" 
				ConstructorDeclaration cd = new ConstructorDeclaration("New", m.Modifier, p, attributes); 
				cd.StartLocation = m.GetDeclarationLocation(startPos);
				cd.EndLocation   = constructorEndLocation;
				cd.Body = (BlockStatement)stmt;
				cd.Body.EndLocation   = endLocation;
				compilationUnit.AddChild(cd);
				
			} else SynErr(224);
			break;
		}
		case 101: {
			lexer.NextToken();

#line  867 "VBNET.ATG" 
			m.Check(Modifiers.VBMethods);
			string name = String.Empty;
			Location startPos = t.Location;
			MethodDeclaration methodDeclaration;List<string> handlesClause = null;
			List<InterfaceImplementation> implementsClause = null;
			AttributeSection returnTypeAttributeSection = null;
			
			Identifier();

#line  874 "VBNET.ATG" 
			name = t.val; 
			TypeParameterList(
#line  875 "VBNET.ATG" 
templates);
			if (la.kind == 25) {
				lexer.NextToken();
				if (StartOf(4)) {
					FormalParameterList(
#line  876 "VBNET.ATG" 
p);
				}
				Expect(26);
			}
			if (la.kind == 49) {
				lexer.NextToken();
				while (la.kind == 28) {
					AttributeSection(
#line  877 "VBNET.ATG" 
out returnTypeAttributeSection);
				}
				TypeName(
#line  877 "VBNET.ATG" 
out type);
			}

#line  879 "VBNET.ATG" 
			if(type == null) {
			type = new TypeReference("System.Object");
			}
			
			if (la.kind == 106 || la.kind == 108) {
				if (la.kind == 108) {
					ImplementsClause(
#line  885 "VBNET.ATG" 
out implementsClause);
				} else {
					HandlesClause(
#line  887 "VBNET.ATG" 
out handlesClause);
				}
			}
			if (
#line  892 "VBNET.ATG" 
IsMustOverride(m)) {
				EndOfStmt();

#line  895 "VBNET.ATG" 
				methodDeclaration = new MethodDeclaration {
				Name = name, Modifier = m.Modifier, TypeReference = type,
				Parameters = p, Attributes = attributes,
				StartLocation = m.GetDeclarationLocation(startPos),
				EndLocation   = t.EndLocation,
				HandlesClause = handlesClause,
				Templates     = templates,
				InterfaceImplementations = implementsClause
				};
				if (returnTypeAttributeSection != null) {
					returnTypeAttributeSection.AttributeTarget = "return";
					methodDeclaration.Attributes.Add(returnTypeAttributeSection);
				}
				compilationUnit.AddChild(methodDeclaration);
				
			} else if (la.kind == 1) {
				lexer.NextToken();

#line  913 "VBNET.ATG" 
				methodDeclaration = new MethodDeclaration {
				Name = name, Modifier = m.Modifier, TypeReference = type,
				Parameters = p, Attributes = attributes,
				StartLocation = m.GetDeclarationLocation(startPos),
				EndLocation   = t.EndLocation,
				Templates     = templates,
				HandlesClause = handlesClause,
				InterfaceImplementations = implementsClause
				};
				if (returnTypeAttributeSection != null) {
					returnTypeAttributeSection.AttributeTarget = "return";
					methodDeclaration.Attributes.Add(returnTypeAttributeSection);
				}
				
				compilationUnit.AddChild(methodDeclaration);
				
				if (ParseMethodBodies) { 
				Block(
#line  930 "VBNET.ATG" 
out stmt);
				Expect(89);
				Expect(101);

#line  932 "VBNET.ATG" 
				} else {
				// don't parse method body
				lexer.SkipCurrentBlock(Tokens.Function); stmt = new BlockStatement();
				}
				methodDeclaration.Body = (BlockStatement)stmt;
				methodDeclaration.Body.StartLocation = methodDeclaration.EndLocation;
				methodDeclaration.Body.EndLocation   = t.EndLocation;
				
				EndOfStmt();
			} else SynErr(225);
			break;
		}
		case 79: {
			lexer.NextToken();

#line  946 "VBNET.ATG" 
			m.Check(Modifiers.VBExternalMethods);
			Location startPos = t.Location;
			CharsetModifier charsetModifer = CharsetModifier.None;
			string library = String.Empty;
			string alias = null;
			string name = String.Empty;
			
			if (StartOf(14)) {
				Charset(
#line  953 "VBNET.ATG" 
out charsetModifer);
			}
			if (la.kind == 168) {
				lexer.NextToken();
				Identifier();

#line  956 "VBNET.ATG" 
				name = t.val; 
				Expect(116);
				Expect(3);

#line  957 "VBNET.ATG" 
				library = t.literalValue as string; 
				if (la.kind == 45) {
					lexer.NextToken();
					Expect(3);

#line  958 "VBNET.ATG" 
					alias = t.literalValue as string; 
				}
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  959 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				EndOfStmt();

#line  962 "VBNET.ATG" 
				DeclareDeclaration declareDeclaration = new DeclareDeclaration(name, m.Modifier, null, p, attributes, library, alias, charsetModifer);
				declareDeclaration.StartLocation = m.GetDeclarationLocation(startPos);
				declareDeclaration.EndLocation   = t.EndLocation;
				compilationUnit.AddChild(declareDeclaration);
				
			} else if (la.kind == 101) {
				lexer.NextToken();
				Identifier();

#line  969 "VBNET.ATG" 
				name = t.val; 
				Expect(116);
				Expect(3);

#line  970 "VBNET.ATG" 
				library = t.literalValue as string; 
				if (la.kind == 45) {
					lexer.NextToken();
					Expect(3);

#line  971 "VBNET.ATG" 
					alias = t.literalValue as string; 
				}
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  972 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				if (la.kind == 49) {
					lexer.NextToken();
					TypeName(
#line  973 "VBNET.ATG" 
out type);
				}
				EndOfStmt();

#line  976 "VBNET.ATG" 
				DeclareDeclaration declareDeclaration = new DeclareDeclaration(name, m.Modifier, type, p, attributes, library, alias, charsetModifer);
				declareDeclaration.StartLocation = m.GetDeclarationLocation(startPos);
				declareDeclaration.EndLocation   = t.EndLocation;
				compilationUnit.AddChild(declareDeclaration);
				
			} else SynErr(226);
			break;
		}
		case 94: {
			lexer.NextToken();

#line  986 "VBNET.ATG" 
			m.Check(Modifiers.VBEvents);
			Location startPos = t.Location;
			EventDeclaration eventDeclaration;
			string name = String.Empty;
			List<InterfaceImplementation> implementsClause = null;
			
			Identifier();

#line  992 "VBNET.ATG" 
			name= t.val; 
			if (la.kind == 49) {
				lexer.NextToken();
				TypeName(
#line  994 "VBNET.ATG" 
out type);
			} else if (StartOf(15)) {
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  996 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
			} else SynErr(227);
			if (la.kind == 108) {
				ImplementsClause(
#line  998 "VBNET.ATG" 
out implementsClause);
			}

#line  1000 "VBNET.ATG" 
			eventDeclaration = new EventDeclaration {
			Name = name, TypeReference = type, Modifier = m.Modifier, 
			Parameters = p, Attributes = attributes, InterfaceImplementations = implementsClause,
			StartLocation = m.GetDeclarationLocation(startPos),
			EndLocation = t.EndLocation
			};
			compilationUnit.AddChild(eventDeclaration);
			
			EndOfStmt();
			break;
		}
		case 2: case 48: case 50: case 51: case 52: case 71: case 96: case 135: case 145: case 170: case 177: case 178: {

#line  1010 "VBNET.ATG" 
			Location startPos = t.Location; 

#line  1012 "VBNET.ATG" 
			m.Check(Modifiers.Fields);
			FieldDeclaration fd = new FieldDeclaration(attributes, null, m.Modifier);
			fd.StartLocation = m.GetDeclarationLocation(startPos); 
			
			IdentifierForFieldDeclaration();

#line  1016 "VBNET.ATG" 
			string name = t.val; 
			VariableDeclaratorPartAfterIdentifier(
#line  1017 "VBNET.ATG" 
variableDeclarators, name);
			while (la.kind == 12) {
				lexer.NextToken();
				VariableDeclarator(
#line  1018 "VBNET.ATG" 
variableDeclarators);
			}
			EndOfStmt();

#line  1021 "VBNET.ATG" 
			fd.EndLocation = t.EndLocation;
			fd.Fields = variableDeclarators;
			SetParent(variableDeclarators, fd);
			compilationUnit.AddChild(fd);
			
			break;
		}
		case 72: {

#line  1027 "VBNET.ATG" 
			m.Check(Modifiers.Fields); 
			lexer.NextToken();

#line  1028 "VBNET.ATG" 
			m.Add(Modifiers.Const, t.Location);  

#line  1030 "VBNET.ATG" 
			FieldDeclaration fd = new FieldDeclaration(attributes, type, m.Modifier);
			fd.StartLocation = m.GetDeclarationLocation(t.Location);
			List<VariableDeclaration> constantDeclarators = new List<VariableDeclaration>();
			
			ConstantDeclarator(
#line  1034 "VBNET.ATG" 
constantDeclarators);
			while (la.kind == 12) {
				lexer.NextToken();
				ConstantDeclarator(
#line  1035 "VBNET.ATG" 
constantDeclarators);
			}

#line  1037 "VBNET.ATG" 
			fd.Fields = constantDeclarators;
			fd.EndLocation = t.Location;
			
			EndOfStmt();

#line  1042 "VBNET.ATG" 
			fd.EndLocation = t.EndLocation;
			compilationUnit.AddChild(fd);
			
			break;
		}
		case 147: {
			lexer.NextToken();

#line  1048 "VBNET.ATG" 
			m.Check(Modifiers.VBProperties);
			Location startPos = t.Location;
			List<InterfaceImplementation> implementsClause = null;
			
			Identifier();

#line  1052 "VBNET.ATG" 
			string propertyName = t.val; 
			if (la.kind == 25) {
				lexer.NextToken();
				if (StartOf(4)) {
					FormalParameterList(
#line  1053 "VBNET.ATG" 
p);
				}
				Expect(26);
			}
			if (la.kind == 49) {
				lexer.NextToken();
				TypeName(
#line  1054 "VBNET.ATG" 
out type);
			}

#line  1056 "VBNET.ATG" 
			if(type == null) {
			type = new TypeReference("System.Object");
			}
			
			if (la.kind == 108) {
				ImplementsClause(
#line  1060 "VBNET.ATG" 
out implementsClause);
			}
			EndOfStmt();
			if (
#line  1064 "VBNET.ATG" 
IsMustOverride(m)) {

#line  1066 "VBNET.ATG" 
				PropertyDeclaration pDecl = new PropertyDeclaration(propertyName, type, m.Modifier, attributes);
				pDecl.StartLocation = m.GetDeclarationLocation(startPos);
				pDecl.EndLocation   = t.Location;
				pDecl.TypeReference = type;
				pDecl.InterfaceImplementations = implementsClause;
				pDecl.Parameters = p;
				compilationUnit.AddChild(pDecl);
				
			} else if (StartOf(16)) {

#line  1076 "VBNET.ATG" 
				PropertyDeclaration pDecl = new PropertyDeclaration(propertyName, type, m.Modifier, attributes);
				pDecl.StartLocation = m.GetDeclarationLocation(startPos);
				pDecl.EndLocation   = t.Location;
				pDecl.BodyStart   = t.Location;
				pDecl.TypeReference = type;
				pDecl.InterfaceImplementations = implementsClause;
				pDecl.Parameters = p;
				PropertyGetRegion getRegion;
				PropertySetRegion setRegion;
				
				AccessorDecls(
#line  1086 "VBNET.ATG" 
out getRegion, out setRegion);
				Expect(89);
				Expect(147);
				EndOfStmt();

#line  1090 "VBNET.ATG" 
				pDecl.GetRegion = getRegion;
				pDecl.SetRegion = setRegion;
				pDecl.BodyEnd = t.EndLocation;
				compilationUnit.AddChild(pDecl);
				
			} else SynErr(228);
			break;
		}
		case 206: {
			lexer.NextToken();

#line  1097 "VBNET.ATG" 
			Location startPos = t.Location; 
			Expect(94);

#line  1099 "VBNET.ATG" 
			m.Check(Modifiers.VBCustomEvents);
			EventAddRemoveRegion eventAccessorDeclaration;
			EventAddRegion addHandlerAccessorDeclaration = null;
			EventRemoveRegion removeHandlerAccessorDeclaration = null;
			EventRaiseRegion raiseEventAccessorDeclaration = null;
			List<InterfaceImplementation> implementsClause = null;
			
			Identifier();

#line  1106 "VBNET.ATG" 
			string customEventName = t.val; 
			Expect(49);
			TypeName(
#line  1107 "VBNET.ATG" 
out type);
			if (la.kind == 108) {
				ImplementsClause(
#line  1108 "VBNET.ATG" 
out implementsClause);
			}
			EndOfStmt();
			while (StartOf(17)) {
				EventAccessorDeclaration(
#line  1111 "VBNET.ATG" 
out eventAccessorDeclaration);

#line  1113 "VBNET.ATG" 
				if(eventAccessorDeclaration is EventAddRegion)
				{
					addHandlerAccessorDeclaration = (EventAddRegion)eventAccessorDeclaration;
				}
				else if(eventAccessorDeclaration is EventRemoveRegion)
				{
					removeHandlerAccessorDeclaration = (EventRemoveRegion)eventAccessorDeclaration;
				}
				else if(eventAccessorDeclaration is EventRaiseRegion)
				{
					raiseEventAccessorDeclaration = (EventRaiseRegion)eventAccessorDeclaration;
				}
				
			}
			Expect(89);
			Expect(94);
			EndOfStmt();

#line  1129 "VBNET.ATG" 
			if(addHandlerAccessorDeclaration == null)
			{
				Error("Need to provide AddHandler accessor.");
			}
			
			if(removeHandlerAccessorDeclaration == null)
			{
				Error("Need to provide RemoveHandler accessor.");
			}
			
			if(raiseEventAccessorDeclaration == null)
			{
				Error("Need to provide RaiseEvent accessor.");
			}
			
			EventDeclaration decl = new EventDeclaration {
				TypeReference = type, Name = customEventName, Modifier = m.Modifier,
				Attributes = attributes,
				StartLocation = m.GetDeclarationLocation(startPos),
				EndLocation = t.EndLocation,
				AddRegion = addHandlerAccessorDeclaration,
				RemoveRegion = removeHandlerAccessorDeclaration,
				RaiseRegion = raiseEventAccessorDeclaration
			};
			compilationUnit.AddChild(decl);
			
			break;
		}
		case 189: case 203: case 204: {

#line  1155 "VBNET.ATG" 
			ConversionType opConversionType = ConversionType.None; 
			if (la.kind == 203 || la.kind == 204) {
				if (la.kind == 204) {
					lexer.NextToken();

#line  1156 "VBNET.ATG" 
					opConversionType = ConversionType.Implicit; 
				} else {
					lexer.NextToken();

#line  1157 "VBNET.ATG" 
					opConversionType = ConversionType.Explicit;
				}
			}
			Expect(189);

#line  1160 "VBNET.ATG" 
			m.Check(Modifiers.VBOperators);
			Location startPos = t.Location;
			TypeReference returnType = NullTypeReference.Instance;
			TypeReference operandType = NullTypeReference.Instance;
			string operandName;
			OverloadableOperatorType operatorType;
			AttributeSection section;
			List<ParameterDeclarationExpression> parameters = new List<ParameterDeclarationExpression>();
			List<AttributeSection> returnTypeAttributes = new List<AttributeSection>();
			
			OverloadableOperator(
#line  1170 "VBNET.ATG" 
out operatorType);
			Expect(25);
			if (la.kind == 56) {
				lexer.NextToken();
			}
			Identifier();

#line  1171 "VBNET.ATG" 
			operandName = t.val; 
			if (la.kind == 49) {
				lexer.NextToken();
				TypeName(
#line  1172 "VBNET.ATG" 
out operandType);
			}

#line  1173 "VBNET.ATG" 
			parameters.Add(new ParameterDeclarationExpression(operandType, operandName, ParameterModifiers.In)); 
			while (la.kind == 12) {
				lexer.NextToken();
				if (la.kind == 56) {
					lexer.NextToken();
				}
				Identifier();

#line  1177 "VBNET.ATG" 
				operandName = t.val; 
				if (la.kind == 49) {
					lexer.NextToken();
					TypeName(
#line  1178 "VBNET.ATG" 
out operandType);
				}

#line  1179 "VBNET.ATG" 
				parameters.Add(new ParameterDeclarationExpression(operandType, operandName, ParameterModifiers.In)); 
			}
			Expect(26);

#line  1182 "VBNET.ATG" 
			Location endPos = t.EndLocation; 
			if (la.kind == 49) {
				lexer.NextToken();
				while (la.kind == 28) {
					AttributeSection(
#line  1183 "VBNET.ATG" 
out section);

#line  1183 "VBNET.ATG" 
					returnTypeAttributes.Add(section); 
				}
				TypeName(
#line  1183 "VBNET.ATG" 
out returnType);

#line  1183 "VBNET.ATG" 
				endPos = t.EndLocation; 
			}
			Expect(1);
			Block(
#line  1185 "VBNET.ATG" 
out stmt);
			Expect(89);
			Expect(189);
			EndOfStmt();

#line  1187 "VBNET.ATG" 
			OperatorDeclaration operatorDeclaration = new OperatorDeclaration {
			Modifier = m.Modifier,
			Attributes = attributes,
			Parameters = parameters,
			TypeReference = returnType,
			OverloadableOperator = operatorType,
			ConversionType = opConversionType,
			ReturnTypeAttributes = returnTypeAttributes,
			Body = (BlockStatement)stmt,
			StartLocation = m.GetDeclarationLocation(startPos),
			EndLocation = endPos
			};
			operatorDeclaration.Body.StartLocation = startPos;
			operatorDeclaration.Body.EndLocation = t.Location;
			compilationUnit.AddChild(operatorDeclaration);
			
			break;
		}
		default: SynErr(229); break;
		}
	}

	void EnumMemberDecl(
#line  727 "VBNET.ATG" 
out FieldDeclaration f) {

#line  729 "VBNET.ATG" 
		Expression expr = null;List<AttributeSection> attributes = new List<AttributeSection>();
		AttributeSection section = null;
		VariableDeclaration varDecl = null;
		
		while (la.kind == 28) {
			AttributeSection(
#line  733 "VBNET.ATG" 
out section);

#line  733 "VBNET.ATG" 
			attributes.Add(section); 
		}
		Identifier();

#line  736 "VBNET.ATG" 
		f = new FieldDeclaration(attributes);
		varDecl = new VariableDeclaration(t.val);
		f.Fields.Add(varDecl);
		f.StartLocation = varDecl.StartLocation = t.Location;
		
		if (la.kind == 11) {
			lexer.NextToken();
			Expr(
#line  741 "VBNET.ATG" 
out expr);

#line  741 "VBNET.ATG" 
			varDecl.Initializer = expr; 
		}
		EndOfStmt();
	}

	void InterfaceMemberDecl() {

#line  618 "VBNET.ATG" 
		TypeReference type =null;
		List<ParameterDeclarationExpression> p = new List<ParameterDeclarationExpression>();
		List<TemplateDefinition> templates = new List<TemplateDefinition>();
		AttributeSection section, returnTypeAttributeSection = null;
		ModifierList mod = new ModifierList();
		List<AttributeSection> attributes = new List<AttributeSection>();
		string name;
		
		if (StartOf(18)) {
			while (la.kind == 28) {
				AttributeSection(
#line  626 "VBNET.ATG" 
out section);

#line  626 "VBNET.ATG" 
				attributes.Add(section); 
			}
			while (StartOf(8)) {
				MemberModifier(
#line  629 "VBNET.ATG" 
mod);
			}
			if (la.kind == 94) {
				lexer.NextToken();

#line  633 "VBNET.ATG" 
				mod.Check(Modifiers.VBInterfaceEvents);
				Location startLocation = t.Location;
				
				Identifier();

#line  636 "VBNET.ATG" 
				name = t.val; 
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  637 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				if (la.kind == 49) {
					lexer.NextToken();
					TypeName(
#line  638 "VBNET.ATG" 
out type);
				}
				EndOfStmt();

#line  641 "VBNET.ATG" 
				EventDeclaration ed = new EventDeclaration {
				Name = name, TypeReference = type, Modifier = mod.Modifier,
				Parameters = p, Attributes = attributes,
				StartLocation = startLocation, EndLocation = t.EndLocation
				};
				compilationUnit.AddChild(ed);
				
			} else if (la.kind == 168) {
				lexer.NextToken();

#line  651 "VBNET.ATG" 
				Location startLocation =  t.Location;
				mod.Check(Modifiers.VBInterfaceMethods);
				
				Identifier();

#line  654 "VBNET.ATG" 
				name = t.val; 
				TypeParameterList(
#line  655 "VBNET.ATG" 
templates);
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  656 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				EndOfStmt();

#line  659 "VBNET.ATG" 
				MethodDeclaration md = new MethodDeclaration {
				Name = name, 
				Modifier = mod.Modifier, 
				Parameters = p,
				Attributes = attributes,
				TypeReference = new TypeReference("", "System.Void"),
				StartLocation = startLocation,
				EndLocation = t.EndLocation,
				Templates = templates
				};
				compilationUnit.AddChild(md);
				
			} else if (la.kind == 101) {
				lexer.NextToken();

#line  674 "VBNET.ATG" 
				mod.Check(Modifiers.VBInterfaceMethods);
				Location startLocation = t.Location;
				
				Identifier();

#line  677 "VBNET.ATG" 
				name = t.val; 
				TypeParameterList(
#line  678 "VBNET.ATG" 
templates);
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  679 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				if (la.kind == 49) {
					lexer.NextToken();
					while (la.kind == 28) {
						AttributeSection(
#line  680 "VBNET.ATG" 
out returnTypeAttributeSection);
					}
					TypeName(
#line  680 "VBNET.ATG" 
out type);
				}

#line  682 "VBNET.ATG" 
				if(type == null) {
				type = new TypeReference("System.Object");
				}
				MethodDeclaration md = new MethodDeclaration {
					Name = name, Modifier = mod.Modifier, 
					TypeReference = type, Parameters = p, Attributes = attributes
				};
				if (returnTypeAttributeSection != null) {
					returnTypeAttributeSection.AttributeTarget = "return";
					md.Attributes.Add(returnTypeAttributeSection);
				}
				md.StartLocation = startLocation;
				md.EndLocation = t.EndLocation;
				md.Templates = templates;
				compilationUnit.AddChild(md);
				
				EndOfStmt();
			} else if (la.kind == 147) {
				lexer.NextToken();

#line  702 "VBNET.ATG" 
				Location startLocation = t.Location;
				mod.Check(Modifiers.VBInterfaceProperties);
				
				Identifier();

#line  705 "VBNET.ATG" 
				name = t.val;  
				if (la.kind == 25) {
					lexer.NextToken();
					if (StartOf(4)) {
						FormalParameterList(
#line  706 "VBNET.ATG" 
p);
					}
					Expect(26);
				}
				if (la.kind == 49) {
					lexer.NextToken();
					TypeName(
#line  707 "VBNET.ATG" 
out type);
				}

#line  709 "VBNET.ATG" 
				if(type == null) {
				type = new TypeReference("System.Object");
				}
				
				EndOfStmt();

#line  715 "VBNET.ATG" 
				PropertyDeclaration pd = new PropertyDeclaration(name, type, mod.Modifier, attributes);
				pd.Parameters = p;
				pd.EndLocation = t.EndLocation;
				pd.StartLocation = startLocation;
				compilationUnit.AddChild(pd);
				
			} else SynErr(230);
		} else if (StartOf(19)) {
			NonModuleDeclaration(
#line  723 "VBNET.ATG" 
mod, attributes);
		} else SynErr(231);
	}

	void Expr(
#line  1593 "VBNET.ATG" 
out Expression expr) {
		DisjunctionExpr(
#line  1595 "VBNET.ATG" 
out expr);
	}

	void ImplementsClause(
#line  1566 "VBNET.ATG" 
out List<InterfaceImplementation> baseInterfaces) {

#line  1568 "VBNET.ATG" 
		baseInterfaces = new List<InterfaceImplementation>();
		TypeReference type = null;
		string memberName = null;
		
		Expect(108);
		NonArrayTypeName(
#line  1573 "VBNET.ATG" 
out type, false);

#line  1574 "VBNET.ATG" 
		if (type != null) memberName = TypeReference.StripLastIdentifierFromType(ref type); 

#line  1575 "VBNET.ATG" 
		baseInterfaces.Add(new InterfaceImplementation(type, memberName)); 
		while (la.kind == 12) {
			lexer.NextToken();
			NonArrayTypeName(
#line  1577 "VBNET.ATG" 
out type, false);

#line  1578 "VBNET.ATG" 
			if (type != null) memberName = TypeReference.StripLastIdentifierFromType(ref type); 

#line  1579 "VBNET.ATG" 
			baseInterfaces.Add(new InterfaceImplementation(type, memberName)); 
		}
	}

	void HandlesClause(
#line  1524 "VBNET.ATG" 
out List<string> handlesClause) {

#line  1526 "VBNET.ATG" 
		handlesClause = new List<string>();
		string name;
		
		Expect(106);
		EventMemberSpecifier(
#line  1529 "VBNET.ATG" 
out name);

#line  1529 "VBNET.ATG" 
		handlesClause.Add(name); 
		while (la.kind == 12) {
			lexer.NextToken();
			EventMemberSpecifier(
#line  1530 "VBNET.ATG" 
out name);

#line  1530 "VBNET.ATG" 
			handlesClause.Add(name); 
		}
	}

	void Block(
#line  2355 "VBNET.ATG" 
out Statement stmt) {

#line  2358 "VBNET.ATG" 
		BlockStatement blockStmt = new BlockStatement();
		/* in snippet parsing mode, t might be null */
		if (t != null) blockStmt.StartLocation = t.Location;
		compilationUnit.BlockStart(blockStmt);
		
		while (StartOf(20) || 
#line  2364 "VBNET.ATG" 
IsEndStmtAhead()) {
			if (
#line  2364 "VBNET.ATG" 
IsEndStmtAhead()) {
				Expect(89);
				EndOfStmt();

#line  2364 "VBNET.ATG" 
				compilationUnit.AddChild(new EndStatement()); 
			} else {
				Statement();
				EndOfStmt();
			}
		}

#line  2369 "VBNET.ATG" 
		stmt = blockStmt;
		if (t != null) blockStmt.EndLocation = t.EndLocation;
		compilationUnit.BlockEnd();
		
	}

	void Charset(
#line  1516 "VBNET.ATG" 
out CharsetModifier charsetModifier) {

#line  1517 "VBNET.ATG" 
		charsetModifier = CharsetModifier.None; 
		if (la.kind == 101 || la.kind == 168) {
		} else if (la.kind == 48) {
			lexer.NextToken();

#line  1518 "VBNET.ATG" 
			charsetModifier = CharsetModifier.Ansi; 
		} else if (la.kind == 51) {
			lexer.NextToken();

#line  1519 "VBNET.ATG" 
			charsetModifier = CharsetModifier.Auto; 
		} else if (la.kind == 177) {
			lexer.NextToken();

#line  1520 "VBNET.ATG" 
			charsetModifier = CharsetModifier.Unicode; 
		} else SynErr(232);
	}

	void IdentifierForFieldDeclaration() {
		switch (la.kind) {
		case 2: {
			lexer.NextToken();
			break;
		}
		case 170: {
			lexer.NextToken();
			break;
		}
		case 52: {
			lexer.NextToken();
			break;
		}
		case 71: {
			lexer.NextToken();
			break;
		}
		case 50: {
			lexer.NextToken();
			break;
		}
		case 48: {
			lexer.NextToken();
			break;
		}
		case 51: {
			lexer.NextToken();
			break;
		}
		case 145: {
			lexer.NextToken();
			break;
		}
		case 177: {
			lexer.NextToken();
			break;
		}
		case 178: {
			lexer.NextToken();
			break;
		}
		case 135: {
			lexer.NextToken();
			break;
		}
		case 96: {
			lexer.NextToken();
			break;
		}
		default: SynErr(233); break;
		}
	}

	void VariableDeclaratorPartAfterIdentifier(
#line  1391 "VBNET.ATG" 
List<VariableDeclaration> fieldDeclaration, string name) {

#line  1393 "VBNET.ATG" 
		Expression expr = null;
		TypeReference type = null;
		ArrayList rank = null;
		List<Expression> dimension = null;
		Location startLocation = t.Location;
		
		if (
#line  1399 "VBNET.ATG" 
IsSize() && !IsDims()) {
			ArrayInitializationModifier(
#line  1399 "VBNET.ATG" 
out dimension);
		}
		if (
#line  1400 "VBNET.ATG" 
IsDims()) {
			ArrayNameModifier(
#line  1400 "VBNET.ATG" 
out rank);
		}
		if (
#line  1402 "VBNET.ATG" 
IsObjectCreation()) {
			Expect(49);
			ObjectCreateExpression(
#line  1402 "VBNET.ATG" 
out expr);

#line  1404 "VBNET.ATG" 
			if (expr is ObjectCreateExpression) {
			type = ((ObjectCreateExpression)expr).CreateType;
			} else {
				type = ((ArrayCreateExpression)expr).CreateType;
			}
			
		} else if (StartOf(21)) {
			if (la.kind == 49) {
				lexer.NextToken();
				TypeName(
#line  1411 "VBNET.ATG" 
out type);

#line  1413 "VBNET.ATG" 
				if (type != null) {
				for (int i = fieldDeclaration.Count - 1; i >= 0; i--) {
					VariableDeclaration vd = fieldDeclaration[i];
					if (vd.TypeReference.Type.Length > 0) break;
					TypeReference newType = type.Clone();
					newType.RankSpecifier = vd.TypeReference.RankSpecifier;
					vd.TypeReference = newType;
				}
				}
				 
			}

#line  1425 "VBNET.ATG" 
			if (type == null && (dimension != null || rank != null)) {
			type = new TypeReference("");
			}
			if (dimension != null) {
				if(type.RankSpecifier != null) {
					Error("array rank only allowed one time");
				} else {
					if (rank == null) {
						type.RankSpecifier = new int[] { dimension.Count - 1 };
					} else {
						rank.Insert(0, dimension.Count - 1);
						type.RankSpecifier = (int[])rank.ToArray(typeof(int));
					}
					expr = new ArrayCreateExpression(type, dimension);
					SetParent(dimension, expr);
				}
			} else if (rank != null) {
				if(type.RankSpecifier != null) {
					Error("array rank only allowed one time");
				} else {
					type.RankSpecifier = (int[])rank.ToArray(typeof(int));
				}
			}
			
			if (la.kind == 11) {
				lexer.NextToken();
				VariableInitializer(
#line  1449 "VBNET.ATG" 
out expr);
			}
		} else SynErr(234);

#line  1452 "VBNET.ATG" 
		VariableDeclaration varDecl = new VariableDeclaration(name, expr, type);
		varDecl.StartLocation = startLocation;
		varDecl.EndLocation = t.Location;
		fieldDeclaration.Add(varDecl);
		
	}

	void VariableDeclarator(
#line  1385 "VBNET.ATG" 
List<VariableDeclaration> fieldDeclaration) {
		Identifier();

#line  1387 "VBNET.ATG" 
		string name = t.val; 
		VariableDeclaratorPartAfterIdentifier(
#line  1388 "VBNET.ATG" 
fieldDeclaration, name);
	}

	void ConstantDeclarator(
#line  1366 "VBNET.ATG" 
List<VariableDeclaration> constantDeclaration) {

#line  1368 "VBNET.ATG" 
		Expression expr = null;
		TypeReference type = null;
		string name = String.Empty;
		Location location;
		
		Identifier();

#line  1373 "VBNET.ATG" 
		name = t.val; location = t.Location; 
		if (la.kind == 49) {
			lexer.NextToken();
			TypeName(
#line  1374 "VBNET.ATG" 
out type);
		}
		Expect(11);
		Expr(
#line  1375 "VBNET.ATG" 
out expr);

#line  1377 "VBNET.ATG" 
		VariableDeclaration f = new VariableDeclaration(name, expr);
		f.TypeReference = type;
		f.StartLocation = location;
		constantDeclaration.Add(f);
		
	}

	void AccessorDecls(
#line  1300 "VBNET.ATG" 
out PropertyGetRegion getBlock, out PropertySetRegion setBlock) {

#line  1302 "VBNET.ATG" 
		List<AttributeSection> attributes = new List<AttributeSection>();
		AttributeSection section;
		getBlock = null;
		setBlock = null; 
		
		while (la.kind == 28) {
			AttributeSection(
#line  1307 "VBNET.ATG" 
out section);

#line  1307 "VBNET.ATG" 
			attributes.Add(section); 
		}
		if (StartOf(22)) {
			GetAccessorDecl(
#line  1309 "VBNET.ATG" 
out getBlock, attributes);
			if (StartOf(23)) {

#line  1311 "VBNET.ATG" 
				attributes = new List<AttributeSection>(); 
				while (la.kind == 28) {
					AttributeSection(
#line  1312 "VBNET.ATG" 
out section);

#line  1312 "VBNET.ATG" 
					attributes.Add(section); 
				}
				SetAccessorDecl(
#line  1313 "VBNET.ATG" 
out setBlock, attributes);
			}
		} else if (StartOf(24)) {
			SetAccessorDecl(
#line  1316 "VBNET.ATG" 
out setBlock, attributes);
			if (StartOf(25)) {

#line  1318 "VBNET.ATG" 
				attributes = new List<AttributeSection>(); 
				while (la.kind == 28) {
					AttributeSection(
#line  1319 "VBNET.ATG" 
out section);

#line  1319 "VBNET.ATG" 
					attributes.Add(section); 
				}
				GetAccessorDecl(
#line  1320 "VBNET.ATG" 
out getBlock, attributes);
			}
		} else SynErr(235);
	}

	void EventAccessorDeclaration(
#line  1263 "VBNET.ATG" 
out EventAddRemoveRegion eventAccessorDeclaration) {

#line  1265 "VBNET.ATG" 
		Statement stmt = null;
		List<ParameterDeclarationExpression> p = new List<ParameterDeclarationExpression>();
		AttributeSection section;
		List<AttributeSection> attributes = new List<AttributeSection>();
		eventAccessorDeclaration = null;
		
		while (la.kind == 28) {
			AttributeSection(
#line  1271 "VBNET.ATG" 
out section);

#line  1271 "VBNET.ATG" 
			attributes.Add(section); 
		}
		if (la.kind == 43) {
			lexer.NextToken();
			if (la.kind == 25) {
				lexer.NextToken();
				if (StartOf(4)) {
					FormalParameterList(
#line  1273 "VBNET.ATG" 
p);
				}
				Expect(26);
			}
			Expect(1);
			Block(
#line  1274 "VBNET.ATG" 
out stmt);
			Expect(89);
			Expect(43);
			EndOfStmt();

#line  1276 "VBNET.ATG" 
			eventAccessorDeclaration = new EventAddRegion(attributes);
			eventAccessorDeclaration.Block = (BlockStatement)stmt;
			eventAccessorDeclaration.Parameters = p;
			
		} else if (la.kind == 153) {
			lexer.NextToken();
			if (la.kind == 25) {
				lexer.NextToken();
				if (StartOf(4)) {
					FormalParameterList(
#line  1281 "VBNET.ATG" 
p);
				}
				Expect(26);
			}
			Expect(1);
			Block(
#line  1282 "VBNET.ATG" 
out stmt);
			Expect(89);
			Expect(153);
			EndOfStmt();

#line  1284 "VBNET.ATG" 
			eventAccessorDeclaration = new EventRemoveRegion(attributes);
			eventAccessorDeclaration.Block = (BlockStatement)stmt;
			eventAccessorDeclaration.Parameters = p;
			
		} else if (la.kind == 150) {
			lexer.NextToken();
			if (la.kind == 25) {
				lexer.NextToken();
				if (StartOf(4)) {
					FormalParameterList(
#line  1289 "VBNET.ATG" 
p);
				}
				Expect(26);
			}
			Expect(1);
			Block(
#line  1290 "VBNET.ATG" 
out stmt);
			Expect(89);
			Expect(150);
			EndOfStmt();

#line  1292 "VBNET.ATG" 
			eventAccessorDeclaration = new EventRaiseRegion(attributes);
			eventAccessorDeclaration.Block = (BlockStatement)stmt;
			eventAccessorDeclaration.Parameters = p;
			
		} else SynErr(236);
	}

	void OverloadableOperator(
#line  1205 "VBNET.ATG" 
out OverloadableOperatorType operatorType) {

#line  1206 "VBNET.ATG" 
		operatorType = OverloadableOperatorType.None; 
		switch (la.kind) {
		case 14: {
			lexer.NextToken();

#line  1208 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Add; 
			break;
		}
		case 15: {
			lexer.NextToken();

#line  1210 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Subtract; 
			break;
		}
		case 16: {
			lexer.NextToken();

#line  1212 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Multiply; 
			break;
		}
		case 17: {
			lexer.NextToken();

#line  1214 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Divide; 
			break;
		}
		case 18: {
			lexer.NextToken();

#line  1216 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.DivideInteger; 
			break;
		}
		case 19: {
			lexer.NextToken();

#line  1218 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Concat; 
			break;
		}
		case 117: {
			lexer.NextToken();

#line  1220 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Like; 
			break;
		}
		case 121: {
			lexer.NextToken();

#line  1222 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Modulus; 
			break;
		}
		case 46: {
			lexer.NextToken();

#line  1224 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.BitwiseAnd; 
			break;
		}
		case 139: {
			lexer.NextToken();

#line  1226 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.BitwiseOr; 
			break;
		}
		case 186: {
			lexer.NextToken();

#line  1228 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.ExclusiveOr; 
			break;
		}
		case 20: {
			lexer.NextToken();

#line  1230 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Power; 
			break;
		}
		case 32: {
			lexer.NextToken();

#line  1232 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.ShiftLeft; 
			break;
		}
		case 33: {
			lexer.NextToken();

#line  1234 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.ShiftRight; 
			break;
		}
		case 11: {
			lexer.NextToken();

#line  1236 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.Equality; 
			break;
		}
		case 29: {
			lexer.NextToken();

#line  1238 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.InEquality; 
			break;
		}
		case 28: {
			lexer.NextToken();

#line  1240 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.LessThan; 
			break;
		}
		case 31: {
			lexer.NextToken();

#line  1242 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.LessThanOrEqual; 
			break;
		}
		case 27: {
			lexer.NextToken();

#line  1244 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.GreaterThan; 
			break;
		}
		case 30: {
			lexer.NextToken();

#line  1246 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.GreaterThanOrEqual; 
			break;
		}
		case 76: {
			lexer.NextToken();

#line  1248 "VBNET.ATG" 
			operatorType = OverloadableOperatorType.CType; 
			break;
		}
		case 2: case 48: case 50: case 51: case 52: case 71: case 96: case 135: case 145: case 170: case 177: case 178: case 206: {
			Identifier();

#line  1252 "VBNET.ATG" 
			string opName = t.val; 
			if (string.Equals(opName, "istrue", StringComparison.InvariantCultureIgnoreCase)) {
				operatorType = OverloadableOperatorType.IsTrue;
			} else if (string.Equals(opName, "isfalse", StringComparison.InvariantCultureIgnoreCase)) {
				operatorType = OverloadableOperatorType.IsFalse;
			} else {
				Error("Invalid operator. Possible operators are '+', '-', 'Not', 'IsTrue', 'IsFalse'.");
			}
			
			break;
		}
		default: SynErr(237); break;
		}
	}

	void GetAccessorDecl(
#line  1326 "VBNET.ATG" 
out PropertyGetRegion getBlock, List<AttributeSection> attributes) {

#line  1327 "VBNET.ATG" 
		Statement stmt = null; Modifiers m; 
		PropertyAccessorAccessModifier(
#line  1329 "VBNET.ATG" 
out m);
		Expect(102);

#line  1331 "VBNET.ATG" 
		Location startLocation = t.Location; 
		Expect(1);
		Block(
#line  1333 "VBNET.ATG" 
out stmt);

#line  1334 "VBNET.ATG" 
		getBlock = new PropertyGetRegion((BlockStatement)stmt, attributes); 
		Expect(89);
		Expect(102);

#line  1336 "VBNET.ATG" 
		getBlock.Modifier = m; 

#line  1337 "VBNET.ATG" 
		getBlock.StartLocation = startLocation; getBlock.EndLocation = t.EndLocation; 
		EndOfStmt();
	}

	void SetAccessorDecl(
#line  1342 "VBNET.ATG" 
out PropertySetRegion setBlock, List<AttributeSection> attributes) {

#line  1344 "VBNET.ATG" 
		Statement stmt = null;
		List<ParameterDeclarationExpression> p = new List<ParameterDeclarationExpression>();
		Modifiers m;
		
		PropertyAccessorAccessModifier(
#line  1349 "VBNET.ATG" 
out m);
		Expect(157);

#line  1351 "VBNET.ATG" 
		Location startLocation = t.Location; 
		if (la.kind == 25) {
			lexer.NextToken();
			if (StartOf(4)) {
				FormalParameterList(
#line  1352 "VBNET.ATG" 
p);
			}
			Expect(26);
		}
		Expect(1);
		Block(
#line  1354 "VBNET.ATG" 
out stmt);

#line  1356 "VBNET.ATG" 
		setBlock = new PropertySetRegion((BlockStatement)stmt, attributes);
		setBlock.Modifier = m;
		setBlock.Parameters = p;
		
		Expect(89);
		Expect(157);

#line  1361 "VBNET.ATG" 
		setBlock.StartLocation = startLocation; setBlock.EndLocation = t.EndLocation; 
		EndOfStmt();
	}

	void PropertyAccessorAccessModifier(
#line  3061 "VBNET.ATG" 
out Modifiers m) {

#line  3062 "VBNET.ATG" 
		m = Modifiers.None; 
		while (StartOf(26)) {
			if (la.kind == 149) {
				lexer.NextToken();

#line  3064 "VBNET.ATG" 
				m |= Modifiers.Public; 
			} else if (la.kind == 148) {
				lexer.NextToken();

#line  3065 "VBNET.ATG" 
				m |= Modifiers.Protected; 
			} else if (la.kind == 100) {
				lexer.NextToken();

#line  3066 "VBNET.ATG" 
				m |= Modifiers.Internal; 
			} else {
				lexer.NextToken();

#line  3067 "VBNET.ATG" 
				m |= Modifiers.Private; 
			}
		}
	}

	void ArrayInitializationModifier(
#line  1460 "VBNET.ATG" 
out List<Expression> arrayModifiers) {

#line  1462 "VBNET.ATG" 
		arrayModifiers = null;
		
		Expect(25);
		InitializationRankList(
#line  1464 "VBNET.ATG" 
out arrayModifiers);
		Expect(26);
	}

	void ArrayNameModifier(
#line  2146 "VBNET.ATG" 
out ArrayList arrayModifiers) {

#line  2148 "VBNET.ATG" 
		arrayModifiers = null;
		
		ArrayTypeModifiers(
#line  2150 "VBNET.ATG" 
out arrayModifiers);
	}

	void ObjectCreateExpression(
#line  1935 "VBNET.ATG" 
out Expression oce) {

#line  1937 "VBNET.ATG" 
		TypeReference type = null;
		Expression initializer = null;
		List<Expression> arguments = null;
		ArrayList dimensions = null;
		oce = null;
		bool canBeNormal; bool canBeReDim;
		
		Expect(128);
		if (StartOf(6)) {
			NonArrayTypeName(
#line  1945 "VBNET.ATG" 
out type, false);
			if (la.kind == 25) {
				lexer.NextToken();
				NormalOrReDimArgumentList(
#line  1946 "VBNET.ATG" 
out arguments, out canBeNormal, out canBeReDim);
				Expect(26);
				if (la.kind == 23 || 
#line  1947 "VBNET.ATG" 
la.kind == Tokens.OpenParenthesis) {
					if (
#line  1947 "VBNET.ATG" 
la.kind == Tokens.OpenParenthesis) {
						ArrayTypeModifiers(
#line  1948 "VBNET.ATG" 
out dimensions);
						CollectionInitializer(
#line  1949 "VBNET.ATG" 
out initializer);
					} else {
						CollectionInitializer(
#line  1950 "VBNET.ATG" 
out initializer);
					}
				}

#line  1952 "VBNET.ATG" 
				if (canBeReDim && !canBeNormal && initializer == null) initializer = new CollectionInitializerExpression(); 
			}
		}

#line  1956 "VBNET.ATG" 
		if (initializer == null) {
		oce = new ObjectCreateExpression(type, arguments);
		SetParent(arguments, oce);
		} else {
			if (dimensions == null) dimensions = new ArrayList();
			dimensions.Insert(0, (arguments == null) ? 0 : Math.Max(arguments.Count - 1, 0));
			type.RankSpecifier = (int[])dimensions.ToArray(typeof(int));
			ArrayCreateExpression ace = new ArrayCreateExpression(type, initializer as CollectionInitializerExpression);
			ace.Arguments = arguments;
			SetParent(arguments, ace);
			oce = ace;
		}
		
		if (la.kind == 183) {

#line  1972 "VBNET.ATG" 
			NamedArgumentExpression memberInitializer = null;
			
			lexer.NextToken();

#line  1976 "VBNET.ATG" 
			CollectionInitializerExpression memberInitializers = new CollectionInitializerExpression();
			memberInitializers.StartLocation = la.Location;
			
			Expect(23);
			MemberInitializer(
#line  1980 "VBNET.ATG" 
out memberInitializer);

#line  1981 "VBNET.ATG" 
			memberInitializers.CreateExpressions.Add(memberInitializer); 
			while (la.kind == 12) {
				lexer.NextToken();
				MemberInitializer(
#line  1983 "VBNET.ATG" 
out memberInitializer);

#line  1984 "VBNET.ATG" 
				memberInitializers.CreateExpressions.Add(memberInitializer); 
			}
			Expect(24);

#line  1988 "VBNET.ATG" 
			memberInitializers.EndLocation = t.Location;
			if(oce is ObjectCreateExpression)
			{
				((ObjectCreateExpression)oce).ObjectInitializer = memberInitializers;
			}
			
		}
	}

	void VariableInitializer(
#line  1488 "VBNET.ATG" 
out Expression initializerExpression) {

#line  1490 "VBNET.ATG" 
		initializerExpression = null;
		
		if (StartOf(27)) {
			Expr(
#line  1492 "VBNET.ATG" 
out initializerExpression);
		} else if (la.kind == 23) {
			CollectionInitializer(
#line  1493 "VBNET.ATG" 
out initializerExpression);
		} else SynErr(238);
	}

	void InitializationRankList(
#line  1468 "VBNET.ATG" 
out List<Expression> rank) {

#line  1470 "VBNET.ATG" 
		rank = new List<Expression>();
		Expression expr = null;
		
		Expr(
#line  1473 "VBNET.ATG" 
out expr);
		if (la.kind == 173) {
			lexer.NextToken();

#line  1474 "VBNET.ATG" 
			EnsureIsZero(expr); 
			Expr(
#line  1475 "VBNET.ATG" 
out expr);
		}

#line  1477 "VBNET.ATG" 
		if (expr != null) { rank.Add(expr); } 
		while (la.kind == 12) {
			lexer.NextToken();
			Expr(
#line  1479 "VBNET.ATG" 
out expr);
			if (la.kind == 173) {
				lexer.NextToken();

#line  1480 "VBNET.ATG" 
				EnsureIsZero(expr); 
				Expr(
#line  1481 "VBNET.ATG" 
out expr);
			}

#line  1483 "VBNET.ATG" 
			if (expr != null) { rank.Add(expr); } 
		}
	}

	void CollectionInitializer(
#line  1497 "VBNET.ATG" 
out Expression outExpr) {

#line  1499 "VBNET.ATG" 
		Expression expr = null;
		CollectionInitializerExpression initializer = new CollectionInitializerExpression();
		
		Expect(23);
		if (StartOf(28)) {
			VariableInitializer(
#line  1504 "VBNET.ATG" 
out expr);

#line  1506 "VBNET.ATG" 
			if (expr != null) { initializer.CreateExpressions.Add(expr); }
			
			while (
#line  1509 "VBNET.ATG" 
NotFinalComma()) {
				Expect(12);
				VariableInitializer(
#line  1509 "VBNET.ATG" 
out expr);

#line  1510 "VBNET.ATG" 
				if (expr != null) { initializer.CreateExpressions.Add(expr); } 
			}
		}
		Expect(24);

#line  1513 "VBNET.ATG" 
		outExpr = initializer; 
	}

	void EventMemberSpecifier(
#line  1583 "VBNET.ATG" 
out string name) {

#line  1584 "VBNET.ATG" 
		string eventName; 
		if (StartOf(13)) {
			Identifier();
		} else if (la.kind == 125) {
			lexer.NextToken();
		} else if (la.kind == 120) {
			lexer.NextToken();
		} else SynErr(239);

#line  1587 "VBNET.ATG" 
		name = t.val; 
		Expect(10);
		IdentifierOrKeyword(
#line  1589 "VBNET.ATG" 
out eventName);

#line  1590 "VBNET.ATG" 
		name = name + "." + eventName; 
	}

	void IdentifierOrKeyword(
#line  2994 "VBNET.ATG" 
out string name) {

#line  2996 "VBNET.ATG" 
		lexer.NextToken(); name = t.val;  
	}

	void DisjunctionExpr(
#line  1779 "VBNET.ATG" 
out Expression outExpr) {

#line  1781 "VBNET.ATG" 
		Expression expr;
		BinaryOperatorType op = BinaryOperatorType.None;
		
		ConjunctionExpr(
#line  1784 "VBNET.ATG" 
out outExpr);
		while (la.kind == 139 || la.kind == 140 || la.kind == 186) {
			if (la.kind == 139) {
				lexer.NextToken();

#line  1787 "VBNET.ATG" 
				op = BinaryOperatorType.BitwiseOr; 
			} else if (la.kind == 140) {
				lexer.NextToken();

#line  1788 "VBNET.ATG" 
				op = BinaryOperatorType.LogicalOr; 
			} else {
				lexer.NextToken();

#line  1789 "VBNET.ATG" 
				op = BinaryOperatorType.ExclusiveOr; 
			}
			ConjunctionExpr(
#line  1791 "VBNET.ATG" 
out expr);

#line  1791 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, op, expr);  
		}
	}

	void AssignmentOperator(
#line  1598 "VBNET.ATG" 
out AssignmentOperatorType op) {

#line  1599 "VBNET.ATG" 
		op = AssignmentOperatorType.None; 
		switch (la.kind) {
		case 11: {
			lexer.NextToken();

#line  1600 "VBNET.ATG" 
			op = AssignmentOperatorType.Assign; 
			break;
		}
		case 42: {
			lexer.NextToken();

#line  1601 "VBNET.ATG" 
			op = AssignmentOperatorType.ConcatString; 
			break;
		}
		case 34: {
			lexer.NextToken();

#line  1602 "VBNET.ATG" 
			op = AssignmentOperatorType.Add; 
			break;
		}
		case 36: {
			lexer.NextToken();

#line  1603 "VBNET.ATG" 
			op = AssignmentOperatorType.Subtract; 
			break;
		}
		case 37: {
			lexer.NextToken();

#line  1604 "VBNET.ATG" 
			op = AssignmentOperatorType.Multiply; 
			break;
		}
		case 38: {
			lexer.NextToken();

#line  1605 "VBNET.ATG" 
			op = AssignmentOperatorType.Divide; 
			break;
		}
		case 39: {
			lexer.NextToken();

#line  1606 "VBNET.ATG" 
			op = AssignmentOperatorType.DivideInteger; 
			break;
		}
		case 35: {
			lexer.NextToken();

#line  1607 "VBNET.ATG" 
			op = AssignmentOperatorType.Power; 
			break;
		}
		case 40: {
			lexer.NextToken();

#line  1608 "VBNET.ATG" 
			op = AssignmentOperatorType.ShiftLeft; 
			break;
		}
		case 41: {
			lexer.NextToken();

#line  1609 "VBNET.ATG" 
			op = AssignmentOperatorType.ShiftRight; 
			break;
		}
		default: SynErr(240); break;
		}
	}

	void SimpleExpr(
#line  1613 "VBNET.ATG" 
out Expression pexpr) {

#line  1614 "VBNET.ATG" 
		string name; 
		SimpleNonInvocationExpression(
#line  1616 "VBNET.ATG" 
out pexpr);
		while (la.kind == 10 || la.kind == 22 || la.kind == 25) {
			if (la.kind == 10) {
				lexer.NextToken();
				IdentifierOrKeyword(
#line  1618 "VBNET.ATG" 
out name);

#line  1618 "VBNET.ATG" 
				pexpr = new MemberReferenceExpression(pexpr, name); 
			} else if (la.kind == 22) {
				lexer.NextToken();
				IdentifierOrKeyword(
#line  1619 "VBNET.ATG" 
out name);

#line  1619 "VBNET.ATG" 
				pexpr = new BinaryOperatorExpression(pexpr, BinaryOperatorType.DictionaryAccess, new PrimitiveExpression(name, name)); 
			} else {
				InvocationExpression(
#line  1620 "VBNET.ATG" 
ref pexpr);
			}
		}
	}

	void SimpleNonInvocationExpression(
#line  1624 "VBNET.ATG" 
out Expression pexpr) {

#line  1626 "VBNET.ATG" 
		Expression expr;
		TypeReference type = null;
		string name = String.Empty;
		pexpr = null;
		
		if (StartOf(29) || 
#line  1679 "VBNET.ATG" 
la.kind == Tokens.If) {
			if (la.kind == 3) {
				lexer.NextToken();

#line  1634 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 4) {
				lexer.NextToken();

#line  1635 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 7) {
				lexer.NextToken();

#line  1636 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 6) {
				lexer.NextToken();

#line  1637 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 5) {
				lexer.NextToken();

#line  1638 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 9) {
				lexer.NextToken();

#line  1639 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 8) {
				lexer.NextToken();

#line  1640 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(t.literalValue, t.val) { LiteralFormat = t.literalFormat };  
			} else if (la.kind == 174) {
				lexer.NextToken();

#line  1642 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(true, "true");  
			} else if (la.kind == 97) {
				lexer.NextToken();

#line  1643 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(false, "false"); 
			} else if (la.kind == 131) {
				lexer.NextToken();

#line  1644 "VBNET.ATG" 
				pexpr = new PrimitiveExpression(null, "null");  
			} else if (la.kind == 25) {
				lexer.NextToken();
				Expr(
#line  1645 "VBNET.ATG" 
out expr);
				Expect(26);

#line  1645 "VBNET.ATG" 
				pexpr = new ParenthesizedExpression(expr); 
			} else if (StartOf(13)) {
				Identifier();

#line  1647 "VBNET.ATG" 
				pexpr = new IdentifierExpression(t.val); 

#line  1648 "VBNET.ATG" 
				pexpr.StartLocation = t.Location; pexpr.EndLocation = t.EndLocation; 
				if (
#line  1649 "VBNET.ATG" 
la.kind == Tokens.OpenParenthesis && Peek(1).kind == Tokens.Of) {
					lexer.NextToken();
					Expect(202);
					TypeArgumentList(
#line  1650 "VBNET.ATG" 
((IdentifierExpression)pexpr).TypeArguments);
					Expect(26);
				}
			} else if (StartOf(30)) {

#line  1652 "VBNET.ATG" 
				string val = String.Empty; 
				if (StartOf(10)) {
					PrimitiveTypeName(
#line  1653 "VBNET.ATG" 
out val);
				} else if (la.kind == 134) {
					lexer.NextToken();

#line  1653 "VBNET.ATG" 
					val = "Object"; 
				} else SynErr(241);
				Expect(10);

#line  1654 "VBNET.ATG" 
				t.val = ""; 
				Identifier();

#line  1654 "VBNET.ATG" 
				pexpr = new MemberReferenceExpression(new TypeReferenceExpression(val), t.val); 
			} else if (la.kind == 120) {
				lexer.NextToken();

#line  1655 "VBNET.ATG" 
				pexpr = new ThisReferenceExpression(); 
			} else if (la.kind == 125 || la.kind == 126) {

#line  1656 "VBNET.ATG" 
				Expression retExpr = null; 
				if (la.kind == 125) {
					lexer.NextToken();

#line  1657 "VBNET.ATG" 
					retExpr = new BaseReferenceExpression(); 
				} else if (la.kind == 126) {
					lexer.NextToken();

#line  1658 "VBNET.ATG" 
					retExpr = new ClassReferenceExpression(); 
				} else SynErr(242);
				Expect(10);
				IdentifierOrKeyword(
#line  1660 "VBNET.ATG" 
out name);

#line  1660 "VBNET.ATG" 
				pexpr = new MemberReferenceExpression(retExpr, name); 
			} else if (la.kind == 200) {
				lexer.NextToken();
				Expect(10);
				Identifier();

#line  1662 "VBNET.ATG" 
				type = new TypeReference(t.val ?? ""); 

#line  1664 "VBNET.ATG" 
				type.IsGlobal = true; 

#line  1665 "VBNET.ATG" 
				pexpr = new TypeReferenceExpression(type); 
			} else if (la.kind == 128) {
				ObjectCreateExpression(
#line  1666 "VBNET.ATG" 
out expr);

#line  1666 "VBNET.ATG" 
				pexpr = expr; 
			} else if (la.kind == 76 || la.kind == 83 || la.kind == 201) {

#line  1668 "VBNET.ATG" 
				CastType castType = CastType.Cast; 
				if (la.kind == 83) {
					lexer.NextToken();
				} else if (la.kind == 76) {
					lexer.NextToken();

#line  1670 "VBNET.ATG" 
					castType = CastType.Conversion; 
				} else if (la.kind == 201) {
					lexer.NextToken();

#line  1671 "VBNET.ATG" 
					castType = CastType.TryCast; 
				} else SynErr(243);
				Expect(25);
				Expr(
#line  1673 "VBNET.ATG" 
out expr);
				Expect(12);
				TypeName(
#line  1673 "VBNET.ATG" 
out type);
				Expect(26);

#line  1674 "VBNET.ATG" 
				pexpr = new CastExpression(type, expr, castType); 
			} else if (StartOf(31)) {
				CastTarget(
#line  1675 "VBNET.ATG" 
out type);
				Expect(25);
				Expr(
#line  1675 "VBNET.ATG" 
out expr);
				Expect(26);

#line  1675 "VBNET.ATG" 
				pexpr = new CastExpression(type, expr, CastType.PrimitiveConversion); 
			} else if (la.kind == 44) {
				lexer.NextToken();
				Expr(
#line  1676 "VBNET.ATG" 
out expr);

#line  1676 "VBNET.ATG" 
				pexpr = new AddressOfExpression(expr); 
			} else if (la.kind == 103) {
				lexer.NextToken();
				Expect(25);
				GetTypeTypeName(
#line  1677 "VBNET.ATG" 
out type);
				Expect(26);

#line  1677 "VBNET.ATG" 
				pexpr = new TypeOfExpression(type); 
			} else if (la.kind == 176) {
				lexer.NextToken();
				SimpleExpr(
#line  1678 "VBNET.ATG" 
out expr);
				Expect(114);
				TypeName(
#line  1678 "VBNET.ATG" 
out type);

#line  1678 "VBNET.ATG" 
				pexpr = new TypeOfIsExpression(expr, type); 
			} else {
				ConditionalExpression(
#line  1679 "VBNET.ATG" 
out pexpr);
			}
		} else if (la.kind == 10) {
			lexer.NextToken();
			IdentifierOrKeyword(
#line  1683 "VBNET.ATG" 
out name);

#line  1683 "VBNET.ATG" 
			pexpr = new MemberReferenceExpression(null, name);
		} else SynErr(244);
	}

	void InvocationExpression(
#line  1721 "VBNET.ATG" 
ref Expression pexpr) {

#line  1722 "VBNET.ATG" 
		List<TypeReference> typeParameters = new List<TypeReference>();
		List<Expression> parameters = null;
		TypeReference type; 
		Expect(25);

#line  1726 "VBNET.ATG" 
		Location start = t.Location; 
		if (la.kind == 202) {
			lexer.NextToken();
			TypeName(
#line  1728 "VBNET.ATG" 
out type);

#line  1728 "VBNET.ATG" 
			if (type != null) typeParameters.Add(type); 
			while (la.kind == 12) {
				lexer.NextToken();
				TypeName(
#line  1731 "VBNET.ATG" 
out type);

#line  1731 "VBNET.ATG" 
				if (type != null) typeParameters.Add(type); 
			}
			Expect(26);
			if (la.kind == 10) {
				lexer.NextToken();
				Identifier();

#line  1736 "VBNET.ATG" 
				pexpr = new MemberReferenceExpression(GetTypeReferenceExpression(pexpr, typeParameters), t.val); 
			} else if (la.kind == 25) {
				lexer.NextToken();
				ArgumentList(
#line  1738 "VBNET.ATG" 
out parameters);
				Expect(26);

#line  1741 "VBNET.ATG" 
				pexpr = CreateInvocationExpression(pexpr, parameters, typeParameters);
				SetParent(parameters, pexpr);
				
			} else SynErr(245);
		} else if (StartOf(32)) {
			ArgumentList(
#line  1745 "VBNET.ATG" 
out parameters);
			Expect(26);

#line  1748 "VBNET.ATG" 
			pexpr = CreateInvocationExpression(pexpr, parameters, typeParameters);
			SetParent(parameters, pexpr);
			
		} else SynErr(246);

#line  1752 "VBNET.ATG" 
		pexpr.StartLocation = start; pexpr.EndLocation = t.Location; 
	}

	void TypeArgumentList(
#line  2182 "VBNET.ATG" 
List<TypeReference> typeArguments) {

#line  2184 "VBNET.ATG" 
		TypeReference typeref;
		
		TypeName(
#line  2186 "VBNET.ATG" 
out typeref);

#line  2186 "VBNET.ATG" 
		if (typeref != null) typeArguments.Add(typeref); 
		while (la.kind == 12) {
			lexer.NextToken();
			TypeName(
#line  2189 "VBNET.ATG" 
out typeref);

#line  2189 "VBNET.ATG" 
			if (typeref != null) typeArguments.Add(typeref); 
		}
	}

	void PrimitiveTypeName(
#line  3001 "VBNET.ATG" 
out string type) {

#line  3002 "VBNET.ATG" 
		type = String.Empty; 
		switch (la.kind) {
		case 53: {
			lexer.NextToken();

#line  3003 "VBNET.ATG" 
			type = "Boolean"; 
			break;
		}
		case 77: {
			lexer.NextToken();

#line  3004 "VBNET.ATG" 
			type = "Date"; 
			break;
		}
		case 66: {
			lexer.NextToken();

#line  3005 "VBNET.ATG" 
			type = "Char"; 
			break;
		}
		case 166: {
			lexer.NextToken();

#line  3006 "VBNET.ATG" 
			type = "String"; 
			break;
		}
		case 78: {
			lexer.NextToken();

#line  3007 "VBNET.ATG" 
			type = "Decimal"; 
			break;
		}
		case 55: {
			lexer.NextToken();

#line  3008 "VBNET.ATG" 
			type = "Byte"; 
			break;
		}
		case 160: {
			lexer.NextToken();

#line  3009 "VBNET.ATG" 
			type = "Short"; 
			break;
		}
		case 112: {
			lexer.NextToken();

#line  3010 "VBNET.ATG" 
			type = "Integer"; 
			break;
		}
		case 118: {
			lexer.NextToken();

#line  3011 "VBNET.ATG" 
			type = "Long"; 
			break;
		}
		case 161: {
			lexer.NextToken();

#line  3012 "VBNET.ATG" 
			type = "Single"; 
			break;
		}
		case 85: {
			lexer.NextToken();

#line  3013 "VBNET.ATG" 
			type = "Double"; 
			break;
		}
		case 193: {
			lexer.NextToken();

#line  3014 "VBNET.ATG" 
			type = "UInteger"; 
			break;
		}
		case 194: {
			lexer.NextToken();

#line  3015 "VBNET.ATG" 
			type = "ULong"; 
			break;
		}
		case 195: {
			lexer.NextToken();

#line  3016 "VBNET.ATG" 
			type = "UShort"; 
			break;
		}
		case 192: {
			lexer.NextToken();

#line  3017 "VBNET.ATG" 
			type = "SByte"; 
			break;
		}
		default: SynErr(247); break;
		}
	}

	void CastTarget(
#line  1757 "VBNET.ATG" 
out TypeReference type) {

#line  1759 "VBNET.ATG" 
		type = null;
		
		switch (la.kind) {
		case 60: {
			lexer.NextToken();

#line  1761 "VBNET.ATG" 
			type = new TypeReference("System.Boolean"); 
			break;
		}
		case 61: {
			lexer.NextToken();

#line  1762 "VBNET.ATG" 
			type = new TypeReference("System.Byte"); 
			break;
		}
		case 196: {
			lexer.NextToken();

#line  1763 "VBNET.ATG" 
			type = new TypeReference("System.SByte"); 
			break;
		}
		case 62: {
			lexer.NextToken();

#line  1764 "VBNET.ATG" 
			type = new TypeReference("System.Char"); 
			break;
		}
		case 63: {
			lexer.NextToken();

#line  1765 "VBNET.ATG" 
			type = new TypeReference("System.DateTime"); 
			break;
		}
		case 65: {
			lexer.NextToken();

#line  1766 "VBNET.ATG" 
			type = new TypeReference("System.Decimal"); 
			break;
		}
		case 64: {
			lexer.NextToken();

#line  1767 "VBNET.ATG" 
			type = new TypeReference("System.Double"); 
			break;
		}
		case 73: {
			lexer.NextToken();

#line  1768 "VBNET.ATG" 
			type = new TypeReference("System.Int16"); 
			break;
		}
		case 67: {
			lexer.NextToken();

#line  1769 "VBNET.ATG" 
			type = new TypeReference("System.Int32"); 
			break;
		}
		case 69: {
			lexer.NextToken();

#line  1770 "VBNET.ATG" 
			type = new TypeReference("System.Int64"); 
			break;
		}
		case 197: {
			lexer.NextToken();

#line  1771 "VBNET.ATG" 
			type = new TypeReference("System.UInt16"); 
			break;
		}
		case 198: {
			lexer.NextToken();

#line  1772 "VBNET.ATG" 
			type = new TypeReference("System.UInt32"); 
			break;
		}
		case 199: {
			lexer.NextToken();

#line  1773 "VBNET.ATG" 
			type = new TypeReference("System.UInt64"); 
			break;
		}
		case 70: {
			lexer.NextToken();

#line  1774 "VBNET.ATG" 
			type = new TypeReference("System.Object"); 
			break;
		}
		case 74: {
			lexer.NextToken();

#line  1775 "VBNET.ATG" 
			type = new TypeReference("System.Single"); 
			break;
		}
		case 75: {
			lexer.NextToken();

#line  1776 "VBNET.ATG" 
			type = new TypeReference("System.String"); 
			break;
		}
		default: SynErr(248); break;
		}
	}

	void GetTypeTypeName(
#line  2081 "VBNET.ATG" 
out TypeReference typeref) {

#line  2082 "VBNET.ATG" 
		ArrayList rank = null; 
		NonArrayTypeName(
#line  2084 "VBNET.ATG" 
out typeref, true);
		ArrayTypeModifiers(
#line  2085 "VBNET.ATG" 
out rank);

#line  2086 "VBNET.ATG" 
		if (rank != null && typeref != null) {
		typeref.RankSpecifier = (int[])rank.ToArray(typeof(int));
		}
		
	}

	void ConditionalExpression(
#line  1687 "VBNET.ATG" 
out Expression expr) {

#line  1689 "VBNET.ATG" 
		ConditionalExpression conditionalExpression = new ConditionalExpression();
		BinaryOperatorExpression binaryOperatorExpression = new BinaryOperatorExpression();
		conditionalExpression.StartLocation = binaryOperatorExpression.StartLocation = la.Location;
		
		Expression condition = null;
		Expression trueExpr = null;
		Expression falseExpr = null;
		
		Expect(107);
		Expect(25);
		Expr(
#line  1698 "VBNET.ATG" 
out condition);
		Expect(12);
		Expr(
#line  1698 "VBNET.ATG" 
out trueExpr);
		if (la.kind == 12) {
			lexer.NextToken();
			Expr(
#line  1698 "VBNET.ATG" 
out falseExpr);
		}
		Expect(26);

#line  1700 "VBNET.ATG" 
		if(falseExpr != null)
		{
			conditionalExpression.Condition = condition;
			conditionalExpression.TrueExpression = trueExpr;
			conditionalExpression.FalseExpression = falseExpr;
			conditionalExpression.EndLocation = t.EndLocation;
			
			expr = conditionalExpression;
		}
		else
		{
			binaryOperatorExpression.Left = condition;
			binaryOperatorExpression.Right = trueExpr;
			binaryOperatorExpression.Op = BinaryOperatorType.NullCoalescing;
			binaryOperatorExpression.EndLocation = t.EndLocation;
			
			expr = binaryOperatorExpression;
		}
		
	}

	void ArgumentList(
#line  2013 "VBNET.ATG" 
out List<Expression> arguments) {

#line  2015 "VBNET.ATG" 
		arguments = new List<Expression>();
		Expression expr = null;
		
		if (StartOf(27)) {
			Argument(
#line  2018 "VBNET.ATG" 
out expr);
		}
		while (la.kind == 12) {
			lexer.NextToken();

#line  2019 "VBNET.ATG" 
			arguments.Add(expr ?? Expression.Null); expr = null; 
			if (StartOf(27)) {
				Argument(
#line  2020 "VBNET.ATG" 
out expr);
			}

#line  2021 "VBNET.ATG" 
			if (expr == null) expr = Expression.Null; 
		}

#line  2023 "VBNET.ATG" 
		if (expr != null) arguments.Add(expr); 
	}

	void ConjunctionExpr(
#line  1795 "VBNET.ATG" 
out Expression outExpr) {

#line  1797 "VBNET.ATG" 
		Expression expr;
		BinaryOperatorType op = BinaryOperatorType.None;
		
		NotExpr(
#line  1800 "VBNET.ATG" 
out outExpr);
		while (la.kind == 46 || la.kind == 47) {
			if (la.kind == 46) {
				lexer.NextToken();

#line  1803 "VBNET.ATG" 
				op = BinaryOperatorType.BitwiseAnd; 
			} else {
				lexer.NextToken();

#line  1804 "VBNET.ATG" 
				op = BinaryOperatorType.LogicalAnd; 
			}
			NotExpr(
#line  1806 "VBNET.ATG" 
out expr);

#line  1806 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, op, expr);  
		}
	}

	void NotExpr(
#line  1810 "VBNET.ATG" 
out Expression outExpr) {

#line  1811 "VBNET.ATG" 
		UnaryOperatorType uop = UnaryOperatorType.None; 
		while (la.kind == 130) {
			lexer.NextToken();

#line  1812 "VBNET.ATG" 
			uop = UnaryOperatorType.Not; 
		}
		ComparisonExpr(
#line  1813 "VBNET.ATG" 
out outExpr);

#line  1814 "VBNET.ATG" 
		if (uop != UnaryOperatorType.None)
		outExpr = new UnaryOperatorExpression(outExpr, uop);
		
	}

	void ComparisonExpr(
#line  1819 "VBNET.ATG" 
out Expression outExpr) {

#line  1821 "VBNET.ATG" 
		Expression expr;
		BinaryOperatorType op = BinaryOperatorType.None;
		
		ShiftExpr(
#line  1824 "VBNET.ATG" 
out outExpr);
		while (StartOf(33)) {
			switch (la.kind) {
			case 28: {
				lexer.NextToken();

#line  1827 "VBNET.ATG" 
				op = BinaryOperatorType.LessThan; 
				break;
			}
			case 27: {
				lexer.NextToken();

#line  1828 "VBNET.ATG" 
				op = BinaryOperatorType.GreaterThan; 
				break;
			}
			case 31: {
				lexer.NextToken();

#line  1829 "VBNET.ATG" 
				op = BinaryOperatorType.LessThanOrEqual; 
				break;
			}
			case 30: {
				lexer.NextToken();

#line  1830 "VBNET.ATG" 
				op = BinaryOperatorType.GreaterThanOrEqual; 
				break;
			}
			case 29: {
				lexer.NextToken();

#line  1831 "VBNET.ATG" 
				op = BinaryOperatorType.InEquality; 
				break;
			}
			case 11: {
				lexer.NextToken();

#line  1832 "VBNET.ATG" 
				op = BinaryOperatorType.Equality; 
				break;
			}
			case 117: {
				lexer.NextToken();

#line  1833 "VBNET.ATG" 
				op = BinaryOperatorType.Like; 
				break;
			}
			case 114: {
				lexer.NextToken();

#line  1834 "VBNET.ATG" 
				op = BinaryOperatorType.ReferenceEquality; 
				break;
			}
			case 191: {
				lexer.NextToken();

#line  1835 "VBNET.ATG" 
				op = BinaryOperatorType.ReferenceInequality; 
				break;
			}
			}
			if (StartOf(34)) {
				ShiftExpr(
#line  1838 "VBNET.ATG" 
out expr);

#line  1838 "VBNET.ATG" 
				outExpr = new BinaryOperatorExpression(outExpr, op, expr);  
			} else if (la.kind == 130) {
				lexer.NextToken();
				ShiftExpr(
#line  1841 "VBNET.ATG" 
out expr);

#line  1841 "VBNET.ATG" 
				outExpr = new BinaryOperatorExpression(outExpr, op, new UnaryOperatorExpression(expr, UnaryOperatorType.Not));  
			} else SynErr(249);
		}
	}

	void ShiftExpr(
#line  1846 "VBNET.ATG" 
out Expression outExpr) {

#line  1848 "VBNET.ATG" 
		Expression expr;
		BinaryOperatorType op = BinaryOperatorType.None;
		
		ConcatenationExpr(
#line  1851 "VBNET.ATG" 
out outExpr);
		while (la.kind == 32 || la.kind == 33) {
			if (la.kind == 32) {
				lexer.NextToken();

#line  1854 "VBNET.ATG" 
				op = BinaryOperatorType.ShiftLeft; 
			} else {
				lexer.NextToken();

#line  1855 "VBNET.ATG" 
				op = BinaryOperatorType.ShiftRight; 
			}
			ConcatenationExpr(
#line  1857 "VBNET.ATG" 
out expr);

#line  1857 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, op, expr);  
		}
	}

	void ConcatenationExpr(
#line  1861 "VBNET.ATG" 
out Expression outExpr) {

#line  1862 "VBNET.ATG" 
		Expression expr; 
		AdditiveExpr(
#line  1864 "VBNET.ATG" 
out outExpr);
		while (la.kind == 19) {
			lexer.NextToken();
			AdditiveExpr(
#line  1864 "VBNET.ATG" 
out expr);

#line  1864 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, BinaryOperatorType.Concat, expr);  
		}
	}

	void AdditiveExpr(
#line  1867 "VBNET.ATG" 
out Expression outExpr) {

#line  1869 "VBNET.ATG" 
		Expression expr;
		BinaryOperatorType op = BinaryOperatorType.None;
		
		ModuloExpr(
#line  1872 "VBNET.ATG" 
out outExpr);
		while (la.kind == 14 || la.kind == 15) {
			if (la.kind == 14) {
				lexer.NextToken();

#line  1875 "VBNET.ATG" 
				op = BinaryOperatorType.Add; 
			} else {
				lexer.NextToken();

#line  1876 "VBNET.ATG" 
				op = BinaryOperatorType.Subtract; 
			}
			ModuloExpr(
#line  1878 "VBNET.ATG" 
out expr);

#line  1878 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, op, expr);  
		}
	}

	void ModuloExpr(
#line  1882 "VBNET.ATG" 
out Expression outExpr) {

#line  1883 "VBNET.ATG" 
		Expression expr; 
		IntegerDivisionExpr(
#line  1885 "VBNET.ATG" 
out outExpr);
		while (la.kind == 121) {
			lexer.NextToken();
			IntegerDivisionExpr(
#line  1885 "VBNET.ATG" 
out expr);

#line  1885 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, BinaryOperatorType.Modulus, expr);  
		}
	}

	void IntegerDivisionExpr(
#line  1888 "VBNET.ATG" 
out Expression outExpr) {

#line  1889 "VBNET.ATG" 
		Expression expr; 
		MultiplicativeExpr(
#line  1891 "VBNET.ATG" 
out outExpr);
		while (la.kind == 18) {
			lexer.NextToken();
			MultiplicativeExpr(
#line  1891 "VBNET.ATG" 
out expr);

#line  1891 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, BinaryOperatorType.DivideInteger, expr);  
		}
	}

	void MultiplicativeExpr(
#line  1894 "VBNET.ATG" 
out Expression outExpr) {

#line  1896 "VBNET.ATG" 
		Expression expr;
		BinaryOperatorType op = BinaryOperatorType.None;
		
		UnaryExpr(
#line  1899 "VBNET.ATG" 
out outExpr);
		while (la.kind == 16 || la.kind == 17) {
			if (la.kind == 16) {
				lexer.NextToken();

#line  1902 "VBNET.ATG" 
				op = BinaryOperatorType.Multiply; 
			} else {
				lexer.NextToken();

#line  1903 "VBNET.ATG" 
				op = BinaryOperatorType.Divide; 
			}
			UnaryExpr(
#line  1905 "VBNET.ATG" 
out expr);

#line  1905 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, op, expr); 
		}
	}

	void UnaryExpr(
#line  1909 "VBNET.ATG" 
out Expression uExpr) {

#line  1911 "VBNET.ATG" 
		Expression expr;
		UnaryOperatorType uop = UnaryOperatorType.None;
		bool isUOp = false;
		
		while (la.kind == 14 || la.kind == 15 || la.kind == 16) {
			if (la.kind == 14) {
				lexer.NextToken();

#line  1915 "VBNET.ATG" 
				uop = UnaryOperatorType.Plus; isUOp = true; 
			} else if (la.kind == 15) {
				lexer.NextToken();

#line  1916 "VBNET.ATG" 
				uop = UnaryOperatorType.Minus; isUOp = true; 
			} else {
				lexer.NextToken();

#line  1917 "VBNET.ATG" 
				uop = UnaryOperatorType.Dereference;  isUOp = true;
			}
		}
		ExponentiationExpr(
#line  1919 "VBNET.ATG" 
out expr);

#line  1921 "VBNET.ATG" 
		if (isUOp) {
		uExpr = new UnaryOperatorExpression(expr, uop);
		} else {
			uExpr = expr;
		}
		
	}

	void ExponentiationExpr(
#line  1929 "VBNET.ATG" 
out Expression outExpr) {

#line  1930 "VBNET.ATG" 
		Expression expr; 
		SimpleExpr(
#line  1932 "VBNET.ATG" 
out outExpr);
		while (la.kind == 20) {
			lexer.NextToken();
			SimpleExpr(
#line  1932 "VBNET.ATG" 
out expr);

#line  1932 "VBNET.ATG" 
			outExpr = new BinaryOperatorExpression(outExpr, BinaryOperatorType.Power, expr);  
		}
	}

	void NormalOrReDimArgumentList(
#line  2027 "VBNET.ATG" 
out List<Expression> arguments, out bool canBeNormal, out bool canBeRedim) {

#line  2029 "VBNET.ATG" 
		arguments = new List<Expression>();
		canBeNormal = true; canBeRedim = !IsNamedAssign();
		Expression expr = null;
		
		if (StartOf(27)) {
			Argument(
#line  2034 "VBNET.ATG" 
out expr);
			if (la.kind == 173) {
				lexer.NextToken();

#line  2035 "VBNET.ATG" 
				EnsureIsZero(expr); canBeNormal = false; 
				Expr(
#line  2036 "VBNET.ATG" 
out expr);
			}
		}
		while (la.kind == 12) {
			lexer.NextToken();

#line  2039 "VBNET.ATG" 
			if (expr == null) canBeRedim = false; 

#line  2040 "VBNET.ATG" 
			arguments.Add(expr ?? Expression.Null); expr = null; 

#line  2041 "VBNET.ATG" 
			canBeRedim &= !IsNamedAssign(); 
			if (StartOf(27)) {
				Argument(
#line  2042 "VBNET.ATG" 
out expr);
				if (la.kind == 173) {
					lexer.NextToken();

#line  2043 "VBNET.ATG" 
					EnsureIsZero(expr); canBeNormal = false; 
					Expr(
#line  2044 "VBNET.ATG" 
out expr);
				}
			}

#line  2046 "VBNET.ATG" 
			if (expr == null) { canBeRedim = false; expr = Expression.Null; } 
		}

#line  2048 "VBNET.ATG" 
		if (expr != null) arguments.Add(expr); else canBeRedim = false; 
	}

	void ArrayTypeModifiers(
#line  2155 "VBNET.ATG" 
out ArrayList arrayModifiers) {

#line  2157 "VBNET.ATG" 
		arrayModifiers = new ArrayList();
		int i = 0;
		
		while (
#line  2160 "VBNET.ATG" 
IsDims()) {
			Expect(25);
			if (la.kind == 12 || la.kind == 26) {
				RankList(
#line  2162 "VBNET.ATG" 
out i);
			}

#line  2164 "VBNET.ATG" 
			arrayModifiers.Add(i);
			
			Expect(26);
		}

#line  2169 "VBNET.ATG" 
		if(arrayModifiers.Count == 0) {
		 arrayModifiers = null;
		}
		
	}

	void MemberInitializer(
#line  1997 "VBNET.ATG" 
out NamedArgumentExpression memberInitializer) {

#line  1999 "VBNET.ATG" 
		memberInitializer = new NamedArgumentExpression();
		memberInitializer.StartLocation = la.Location;
		Expression initExpr = null;
		string name = null;
		
		Expect(10);
		IdentifierOrKeyword(
#line  2004 "VBNET.ATG" 
out name);
		Expect(11);
		Expr(
#line  2004 "VBNET.ATG" 
out initExpr);

#line  2006 "VBNET.ATG" 
		memberInitializer.Name = name;
		memberInitializer.Expression = initExpr;
		memberInitializer.EndLocation = t.EndLocation;
		
	}

	void Argument(
#line  2052 "VBNET.ATG" 
out Expression argumentexpr) {

#line  2054 "VBNET.ATG" 
		Expression expr;
		argumentexpr = null;
		string name;
		
		if (
#line  2058 "VBNET.ATG" 
IsNamedAssign()) {
			Identifier();

#line  2058 "VBNET.ATG" 
			name = t.val;  
			Expect(13);
			Expect(11);
			Expr(
#line  2058 "VBNET.ATG" 
out expr);

#line  2060 "VBNET.ATG" 
			argumentexpr = new NamedArgumentExpression(name, expr);
			
		} else if (StartOf(27)) {
			Expr(
#line  2063 "VBNET.ATG" 
out argumentexpr);
		} else SynErr(250);
	}

	void QualIdentAndTypeArguments(
#line  2129 "VBNET.ATG" 
out TypeReference typeref, bool canBeUnbound) {

#line  2130 "VBNET.ATG" 
		string name; typeref = null; 
		Qualident(
#line  2132 "VBNET.ATG" 
out name);

#line  2133 "VBNET.ATG" 
		typeref = new TypeReference(name); 
		if (
#line  2134 "VBNET.ATG" 
la.kind == Tokens.OpenParenthesis && Peek(1).kind == Tokens.Of) {
			lexer.NextToken();
			Expect(202);
			if (
#line  2136 "VBNET.ATG" 
canBeUnbound && (la.kind == Tokens.CloseParenthesis || la.kind == Tokens.Comma)) {

#line  2137 "VBNET.ATG" 
				typeref.GenericTypes.Add(NullTypeReference.Instance); 
				while (la.kind == 12) {
					lexer.NextToken();

#line  2138 "VBNET.ATG" 
					typeref.GenericTypes.Add(NullTypeReference.Instance); 
				}
			} else if (StartOf(6)) {
				TypeArgumentList(
#line  2139 "VBNET.ATG" 
typeref.GenericTypes);
			} else SynErr(251);
			Expect(26);
		}
	}

	void RankList(
#line  2176 "VBNET.ATG" 
out int i) {

#line  2177 "VBNET.ATG" 
		i = 0; 
		while (la.kind == 12) {
			lexer.NextToken();

#line  2178 "VBNET.ATG" 
			++i; 
		}
	}

	void Attribute(
#line  2217 "VBNET.ATG" 
out ASTAttribute attribute) {

#line  2218 "VBNET.ATG" 
		string name;
		List<Expression> positional = new List<Expression>();
		List<NamedArgumentExpression> named = new List<NamedArgumentExpression>();
		
		if (la.kind == 200) {
			lexer.NextToken();
			Expect(10);
		}
		Qualident(
#line  2223 "VBNET.ATG" 
out name);
		if (la.kind == 25) {
			AttributeArguments(
#line  2224 "VBNET.ATG" 
positional, named);
		}

#line  2226 "VBNET.ATG" 
		attribute  = new ASTAttribute(name, positional, named);
		SetParent(positional, attribute);			
		SetParent(named, attribute);			
		
	}

	void AttributeArguments(
#line  2233 "VBNET.ATG" 
List<Expression> positional, List<NamedArgumentExpression> named) {

#line  2235 "VBNET.ATG" 
		bool nameFound = false;
		string name = "";
		Expression expr;
		
		Expect(25);
		if (
#line  2241 "VBNET.ATG" 
IsNotClosingParenthesis()) {
			if (
#line  2243 "VBNET.ATG" 
IsNamedAssign()) {

#line  2243 "VBNET.ATG" 
				nameFound = true; 
				IdentifierOrKeyword(
#line  2244 "VBNET.ATG" 
out name);
				if (la.kind == 13) {
					lexer.NextToken();
				}
				Expect(11);
			}
			Expr(
#line  2246 "VBNET.ATG" 
out expr);

#line  2248 "VBNET.ATG" 
			if (expr != null) {
			if (string.IsNullOrEmpty(name)) { positional.Add(expr); }
			else { named.Add(new NamedArgumentExpression(name, expr)); name = ""; }
			}
			
			while (la.kind == 12) {
				lexer.NextToken();
				if (
#line  2256 "VBNET.ATG" 
IsNamedAssign()) {

#line  2256 "VBNET.ATG" 
					nameFound = true; 
					IdentifierOrKeyword(
#line  2257 "VBNET.ATG" 
out name);
					if (la.kind == 13) {
						lexer.NextToken();
					}
					Expect(11);
				} else if (StartOf(27)) {

#line  2259 "VBNET.ATG" 
					if (nameFound) Error("no positional argument after named argument"); 
				} else SynErr(252);
				Expr(
#line  2260 "VBNET.ATG" 
out expr);

#line  2260 "VBNET.ATG" 
				if (expr != null) { if(name == "") positional.Add(expr);
				else { named.Add(new NamedArgumentExpression(name, expr)); name = ""; }
				}
				
			}
		}
		Expect(26);
	}

	void FormalParameter(
#line  2317 "VBNET.ATG" 
out ParameterDeclarationExpression p) {

#line  2319 "VBNET.ATG" 
		AttributeSection section;
		List<AttributeSection> attributes = new List<AttributeSection>();
		TypeReference type = null;
		ParamModifierList mod = new ParamModifierList(this);
		Expression expr = null;
		p = null;
		ArrayList arrayModifiers = null;
		
		while (la.kind == 28) {
			AttributeSection(
#line  2328 "VBNET.ATG" 
out section);

#line  2328 "VBNET.ATG" 
			attributes.Add(section); 
		}
		while (StartOf(35)) {
			ParameterModifier(
#line  2329 "VBNET.ATG" 
mod);
		}
		Identifier();

#line  2330 "VBNET.ATG" 
		string parameterName = t.val; 
		if (
#line  2331 "VBNET.ATG" 
IsDims()) {
			ArrayTypeModifiers(
#line  2331 "VBNET.ATG" 
out arrayModifiers);
		}
		if (la.kind == 49) {
			lexer.NextToken();
			TypeName(
#line  2332 "VBNET.ATG" 
out type);
		}

#line  2334 "VBNET.ATG" 
		if(type != null) {
		if (arrayModifiers != null) {
			if (type.RankSpecifier != null) {
				Error("array rank only allowed one time");
			} else {
				type.RankSpecifier = (int[])arrayModifiers.ToArray(typeof(int));
			}
		}
		} else {
			type = new TypeReference("System.Object", arrayModifiers == null ? null : (int[])arrayModifiers.ToArray(typeof(int)));
		}
		
		if (la.kind == 11) {
			lexer.NextToken();
			Expr(
#line  2346 "VBNET.ATG" 
out expr);
		}

#line  2348 "VBNET.ATG" 
		mod.Check();
		p = new ParameterDeclarationExpression(type, parameterName, mod.Modifier, expr);
		p.Attributes = attributes;
		
	}

	void ParameterModifier(
#line  3020 "VBNET.ATG" 
ParamModifierList m) {
		if (la.kind == 56) {
			lexer.NextToken();

#line  3021 "VBNET.ATG" 
			m.Add(ParameterModifiers.In); 
		} else if (la.kind == 54) {
			lexer.NextToken();

#line  3022 "VBNET.ATG" 
			m.Add(ParameterModifiers.Ref); 
		} else if (la.kind == 138) {
			lexer.NextToken();

#line  3023 "VBNET.ATG" 
			m.Add(ParameterModifiers.Optional); 
		} else if (la.kind == 144) {
			lexer.NextToken();

#line  3024 "VBNET.ATG" 
			m.Add(ParameterModifiers.Params); 
		} else SynErr(253);
	}

	void Statement() {

#line  2377 "VBNET.ATG" 
		Statement stmt = null;
		Location startPos = la.Location;
		string label = String.Empty;
		
		
		if (la.kind == 1 || la.kind == 13) {
		} else if (
#line  2383 "VBNET.ATG" 
IsLabel()) {
			LabelName(
#line  2383 "VBNET.ATG" 
out label);

#line  2385 "VBNET.ATG" 
			compilationUnit.AddChild(new LabelStatement(t.val));
			
			Expect(13);
			Statement();
		} else if (StartOf(36)) {
			EmbeddedStatement(
#line  2388 "VBNET.ATG" 
out stmt);

#line  2388 "VBNET.ATG" 
			compilationUnit.AddChild(stmt); 
		} else if (StartOf(37)) {
			LocalDeclarationStatement(
#line  2389 "VBNET.ATG" 
out stmt);

#line  2389 "VBNET.ATG" 
			compilationUnit.AddChild(stmt); 
		} else SynErr(254);

#line  2392 "VBNET.ATG" 
		if (stmt != null) {
		stmt.StartLocation = startPos;
		stmt.EndLocation = t.Location;
		}
		
	}

	void LabelName(
#line  2798 "VBNET.ATG" 
out string name) {

#line  2800 "VBNET.ATG" 
		name = String.Empty;
		
		if (StartOf(13)) {
			Identifier();

#line  2802 "VBNET.ATG" 
			name = t.val; 
		} else if (la.kind == 5) {
			lexer.NextToken();

#line  2803 "VBNET.ATG" 
			name = t.val; 
		} else SynErr(255);
	}

	void EmbeddedStatement(
#line  2432 "VBNET.ATG" 
out Statement statement) {

#line  2434 "VBNET.ATG" 
		Statement embeddedStatement = null;
		statement = null;
		Expression expr = null;
		string name = String.Empty;
		List<Expression> p = null;
		
		switch (la.kind) {
		case 95: {
			lexer.NextToken();

#line  2440 "VBNET.ATG" 
			ExitType exitType = ExitType.None; 
			switch (la.kind) {
			case 168: {
				lexer.NextToken();

#line  2442 "VBNET.ATG" 
				exitType = ExitType.Sub; 
				break;
			}
			case 101: {
				lexer.NextToken();

#line  2444 "VBNET.ATG" 
				exitType = ExitType.Function; 
				break;
			}
			case 147: {
				lexer.NextToken();

#line  2446 "VBNET.ATG" 
				exitType = ExitType.Property; 
				break;
			}
			case 84: {
				lexer.NextToken();

#line  2448 "VBNET.ATG" 
				exitType = ExitType.Do; 
				break;
			}
			case 99: {
				lexer.NextToken();

#line  2450 "VBNET.ATG" 
				exitType = ExitType.For; 
				break;
			}
			case 175: {
				lexer.NextToken();

#line  2452 "VBNET.ATG" 
				exitType = ExitType.Try; 
				break;
			}
			case 182: {
				lexer.NextToken();

#line  2454 "VBNET.ATG" 
				exitType = ExitType.While; 
				break;
			}
			case 156: {
				lexer.NextToken();

#line  2456 "VBNET.ATG" 
				exitType = ExitType.Select; 
				break;
			}
			default: SynErr(256); break;
			}

#line  2458 "VBNET.ATG" 
			statement = new ExitStatement(exitType); 
			break;
		}
		case 175: {
			TryStatement(
#line  2459 "VBNET.ATG" 
out statement);
			break;
		}
		case 188: {
			lexer.NextToken();

#line  2460 "VBNET.ATG" 
			ContinueType continueType = ContinueType.None; 
			if (la.kind == 84 || la.kind == 99 || la.kind == 182) {
				if (la.kind == 84) {
					lexer.NextToken();

#line  2460 "VBNET.ATG" 
					continueType = ContinueType.Do; 
				} else if (la.kind == 99) {
					lexer.NextToken();

#line  2460 "VBNET.ATG" 
					continueType = ContinueType.For; 
				} else {
					lexer.NextToken();

#line  2460 "VBNET.ATG" 
					continueType = ContinueType.While; 
				}
			}

#line  2460 "VBNET.ATG" 
			statement = new ContinueStatement(continueType); 
			break;
		}
		case 172: {
			lexer.NextToken();
			if (StartOf(27)) {
				Expr(
#line  2462 "VBNET.ATG" 
out expr);
			}

#line  2462 "VBNET.ATG" 
			statement = new ThrowStatement(expr); 
			break;
		}
		case 155: {
			lexer.NextToken();
			if (StartOf(27)) {
				Expr(
#line  2464 "VBNET.ATG" 
out expr);
			}

#line  2464 "VBNET.ATG" 
			statement = new ReturnStatement(expr); 
			break;
		}
		case 169: {
			lexer.NextToken();
			Expr(
#line  2466 "VBNET.ATG" 
out expr);
			EndOfStmt();
			Block(
#line  2466 "VBNET.ATG" 
out embeddedStatement);
			Expect(89);
			Expect(169);

#line  2467 "VBNET.ATG" 
			statement = new LockStatement(expr, embeddedStatement); 
			break;
		}
		case 150: {
			lexer.NextToken();
			Identifier();

#line  2469 "VBNET.ATG" 
			name = t.val; 
			if (la.kind == 25) {
				lexer.NextToken();
				if (StartOf(32)) {
					ArgumentList(
#line  2470 "VBNET.ATG" 
out p);
				}
				Expect(26);
			}

#line  2472 "VBNET.ATG" 
			statement = new RaiseEventStatement(name, p);
			SetParent(p, statement);
			
			break;
		}
		case 183: {
			WithStatement(
#line  2476 "VBNET.ATG" 
out statement);
			break;
		}
		case 43: {
			lexer.NextToken();

#line  2478 "VBNET.ATG" 
			Expression handlerExpr = null; 
			Expr(
#line  2479 "VBNET.ATG" 
out expr);
			Expect(12);
			Expr(
#line  2479 "VBNET.ATG" 
out handlerExpr);

#line  2481 "VBNET.ATG" 
			statement = new AddHandlerStatement(expr, handlerExpr);
			
			break;
		}
		case 153: {
			lexer.NextToken();

#line  2484 "VBNET.ATG" 
			Expression handlerExpr = null; 
			Expr(
#line  2485 "VBNET.ATG" 
out expr);
			Expect(12);
			Expr(
#line  2485 "VBNET.ATG" 
out handlerExpr);

#line  2487 "VBNET.ATG" 
			statement = new RemoveHandlerStatement(expr, handlerExpr);
			
			break;
		}
		case 182: {
			lexer.NextToken();
			Expr(
#line  2490 "VBNET.ATG" 
out expr);
			EndOfStmt();
			Block(
#line  2491 "VBNET.ATG" 
out embeddedStatement);
			Expect(89);
			Expect(182);

#line  2493 "VBNET.ATG" 
			statement = new DoLoopStatement(expr, embeddedStatement, ConditionType.While, ConditionPosition.Start);
			
			break;
		}
		case 84: {
			lexer.NextToken();

#line  2498 "VBNET.ATG" 
			ConditionType conditionType = ConditionType.None;
			
			if (la.kind == 178 || la.kind == 182) {
				WhileOrUntil(
#line  2501 "VBNET.ATG" 
out conditionType);
				Expr(
#line  2501 "VBNET.ATG" 
out expr);
				EndOfStmt();
				Block(
#line  2502 "VBNET.ATG" 
out embeddedStatement);
				Expect(119);

#line  2505 "VBNET.ATG" 
				statement = new DoLoopStatement(expr, 
				                               embeddedStatement, 
				                               conditionType == ConditionType.While ? ConditionType.DoWhile : conditionType, 
				                               ConditionPosition.Start);
				
			} else if (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
				Block(
#line  2512 "VBNET.ATG" 
out embeddedStatement);
				Expect(119);
				if (la.kind == 178 || la.kind == 182) {
					WhileOrUntil(
#line  2513 "VBNET.ATG" 
out conditionType);
					Expr(
#line  2513 "VBNET.ATG" 
out expr);
				}

#line  2515 "VBNET.ATG" 
				statement = new DoLoopStatement(expr, embeddedStatement, conditionType, ConditionPosition.End);
				
			} else SynErr(257);
			break;
		}
		case 99: {
			lexer.NextToken();

#line  2520 "VBNET.ATG" 
			Expression group = null;
			TypeReference typeReference;
			string        typeName;
			Location startLocation = t.Location;
			
			if (la.kind == 86) {
				lexer.NextToken();
				LoopControlVariable(
#line  2527 "VBNET.ATG" 
out typeReference, out typeName);
				Expect(110);
				Expr(
#line  2528 "VBNET.ATG" 
out group);
				EndOfStmt();
				Block(
#line  2529 "VBNET.ATG" 
out embeddedStatement);
				Expect(129);
				if (StartOf(27)) {
					Expr(
#line  2530 "VBNET.ATG" 
out expr);
				}

#line  2532 "VBNET.ATG" 
				statement = new ForeachStatement(typeReference, 
				                                typeName,
				                                group, 
				                                embeddedStatement, 
				                                expr);
				statement.StartLocation = startLocation;
				statement.EndLocation   = t.EndLocation;
				
				
			} else if (StartOf(13)) {

#line  2543 "VBNET.ATG" 
				Expression start = null;
				Expression end = null;
				Expression step = null;
				Expression nextExpr = null;List<Expression> nextExpressions = null;
				
				LoopControlVariable(
#line  2548 "VBNET.ATG" 
out typeReference, out typeName);
				Expect(11);
				Expr(
#line  2549 "VBNET.ATG" 
out start);
				Expect(173);
				Expr(
#line  2549 "VBNET.ATG" 
out end);
				if (la.kind == 163) {
					lexer.NextToken();
					Expr(
#line  2549 "VBNET.ATG" 
out step);
				}
				EndOfStmt();
				Block(
#line  2550 "VBNET.ATG" 
out embeddedStatement);
				Expect(129);
				if (StartOf(27)) {
					Expr(
#line  2553 "VBNET.ATG" 
out nextExpr);

#line  2555 "VBNET.ATG" 
					nextExpressions = new List<Expression>();
					nextExpressions.Add(nextExpr);
					
					while (la.kind == 12) {
						lexer.NextToken();
						Expr(
#line  2558 "VBNET.ATG" 
out nextExpr);

#line  2558 "VBNET.ATG" 
						nextExpressions.Add(nextExpr); 
					}
				}

#line  2561 "VBNET.ATG" 
				statement = new ForNextStatement(typeReference, typeName, start, end, step, embeddedStatement, nextExpressions);
				SetParent(nextExpressions, statement);
				
			} else SynErr(258);
			break;
		}
		case 93: {
			lexer.NextToken();
			Expr(
#line  2566 "VBNET.ATG" 
out expr);

#line  2566 "VBNET.ATG" 
			statement = new ErrorStatement(expr); 
			break;
		}
		case 152: {
			lexer.NextToken();

#line  2568 "VBNET.ATG" 
			bool isPreserve = false; 
			if (la.kind == 145) {
				lexer.NextToken();

#line  2568 "VBNET.ATG" 
				isPreserve = true; 
			}
			ReDimClause(
#line  2569 "VBNET.ATG" 
out expr);

#line  2571 "VBNET.ATG" 
			ReDimStatement reDimStatement = new ReDimStatement(isPreserve);
			statement = reDimStatement;
			SafeAdd(reDimStatement, reDimStatement.ReDimClauses, expr as InvocationExpression);
			
			while (la.kind == 12) {
				lexer.NextToken();
				ReDimClause(
#line  2575 "VBNET.ATG" 
out expr);

#line  2576 "VBNET.ATG" 
				SafeAdd(reDimStatement, reDimStatement.ReDimClauses, expr as InvocationExpression); 
			}
			break;
		}
		case 92: {
			lexer.NextToken();
			Expr(
#line  2580 "VBNET.ATG" 
out expr);

#line  2582 "VBNET.ATG" 
			EraseStatement eraseStatement = new EraseStatement();
			if (expr != null) { SafeAdd(eraseStatement, eraseStatement.Expressions, expr);}
			
			while (la.kind == 12) {
				lexer.NextToken();
				Expr(
#line  2585 "VBNET.ATG" 
out expr);

#line  2585 "VBNET.ATG" 
				if (expr != null) { SafeAdd(eraseStatement, eraseStatement.Expressions, expr); }
			}

#line  2586 "VBNET.ATG" 
			statement = eraseStatement; 
			break;
		}
		case 164: {
			lexer.NextToken();

#line  2588 "VBNET.ATG" 
			statement = new StopStatement(); 
			break;
		}
		case 107: {
			lexer.NextToken();

#line  2590 "VBNET.ATG" 
			Location ifStartLocation = t.Location; 
			Expr(
#line  2590 "VBNET.ATG" 
out expr);
			if (la.kind == 171) {
				lexer.NextToken();
			}
			if (la.kind == 1 || la.kind == 13) {
				EndOfStmt();
				Block(
#line  2593 "VBNET.ATG" 
out embeddedStatement);

#line  2595 "VBNET.ATG" 
				IfElseStatement ifStatement = new IfElseStatement(expr, embeddedStatement);
				ifStatement.StartLocation = ifStartLocation;
				Location elseIfStart;
				
				while (la.kind == 88 || 
#line  2601 "VBNET.ATG" 
IsElseIf()) {
					if (
#line  2601 "VBNET.ATG" 
IsElseIf()) {
						Expect(87);

#line  2601 "VBNET.ATG" 
						elseIfStart = t.Location; 
						Expect(107);
					} else {
						lexer.NextToken();

#line  2602 "VBNET.ATG" 
						elseIfStart = t.Location; 
					}

#line  2604 "VBNET.ATG" 
					Expression condition = null; Statement block = null; 
					Expr(
#line  2605 "VBNET.ATG" 
out condition);
					if (la.kind == 171) {
						lexer.NextToken();
					}
					EndOfStmt();
					Block(
#line  2606 "VBNET.ATG" 
out block);

#line  2608 "VBNET.ATG" 
					ElseIfSection elseIfSection = new ElseIfSection(condition, block);
					elseIfSection.StartLocation = elseIfStart;
					elseIfSection.EndLocation = t.Location;
					elseIfSection.Parent = ifStatement;
					ifStatement.ElseIfSections.Add(elseIfSection);
					
				}
				if (la.kind == 87) {
					lexer.NextToken();
					EndOfStmt();
					Block(
#line  2617 "VBNET.ATG" 
out embeddedStatement);

#line  2619 "VBNET.ATG" 
					ifStatement.FalseStatement.Add(embeddedStatement);
					
				}
				Expect(89);
				Expect(107);

#line  2623 "VBNET.ATG" 
				ifStatement.EndLocation = t.Location;
				statement = ifStatement;
				
			} else if (StartOf(38)) {

#line  2628 "VBNET.ATG" 
				IfElseStatement ifStatement = new IfElseStatement(expr);
				ifStatement.StartLocation = ifStartLocation;
				
				SingleLineStatementList(
#line  2631 "VBNET.ATG" 
ifStatement.TrueStatement);

#line  2633 "VBNET.ATG" 
				SetParent(ifStatement.TrueStatement, ifStatement);
				
				if (la.kind == 87) {
					lexer.NextToken();
					if (StartOf(38)) {
						SingleLineStatementList(
#line  2637 "VBNET.ATG" 
ifStatement.FalseStatement);
					}
				}

#line  2639 "VBNET.ATG" 
				ifStatement.EndLocation = t.Location; statement = ifStatement; 
			} else SynErr(259);
			break;
		}
		case 156: {
			lexer.NextToken();
			if (la.kind == 58) {
				lexer.NextToken();
			}
			Expr(
#line  2642 "VBNET.ATG" 
out expr);
			EndOfStmt();

#line  2643 "VBNET.ATG" 
			List<SwitchSection> selectSections = new List<SwitchSection>();
			Statement block = null;
			
			while (la.kind == 58) {

#line  2647 "VBNET.ATG" 
				List<CaseLabel> caseClauses = null; Location caseLocation = la.Location; 
				lexer.NextToken();
				CaseClauses(
#line  2648 "VBNET.ATG" 
out caseClauses);
				if (
#line  2648 "VBNET.ATG" 
IsNotStatementSeparator()) {
					lexer.NextToken();
				}
				EndOfStmt();

#line  2650 "VBNET.ATG" 
				SwitchSection selectSection = new SwitchSection(caseClauses);
				SetParent(caseClauses, selectSection);
				selectSection.StartLocation = caseLocation;
				
				Block(
#line  2654 "VBNET.ATG" 
out block);

#line  2656 "VBNET.ATG" 
				selectSection.Children = block.Children;
				selectSection.EndLocation = t.EndLocation;
				selectSections.Add(selectSection);
				
			}

#line  2662 "VBNET.ATG" 
			statement = new SwitchStatement(expr, selectSections);
			SetParent(selectSections, statement);
			
			Expect(89);
			Expect(156);
			break;
		}
		case 136: {

#line  2666 "VBNET.ATG" 
			OnErrorStatement onErrorStatement = null; 
			OnErrorStatement(
#line  2667 "VBNET.ATG" 
out onErrorStatement);

#line  2667 "VBNET.ATG" 
			statement = onErrorStatement; 
			break;
		}
		case 105: {

#line  2668 "VBNET.ATG" 
			GotoStatement goToStatement = null; 
			GotoStatement(
#line  2669 "VBNET.ATG" 
out goToStatement);

#line  2669 "VBNET.ATG" 
			statement = goToStatement; 
			break;
		}
		case 154: {

#line  2670 "VBNET.ATG" 
			ResumeStatement resumeStatement = null; 
			ResumeStatement(
#line  2671 "VBNET.ATG" 
out resumeStatement);

#line  2671 "VBNET.ATG" 
			statement = resumeStatement; 
			break;
		}
		case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9: case 10: case 25: case 44: case 48: case 50: case 51: case 52: case 53: case 55: case 60: case 61: case 62: case 63: case 64: case 65: case 66: case 67: case 69: case 70: case 71: case 73: case 74: case 75: case 76: case 77: case 78: case 83: case 85: case 96: case 97: case 103: case 112: case 118: case 120: case 125: case 126: case 128: case 131: case 134: case 135: case 145: case 160: case 161: case 166: case 170: case 174: case 176: case 177: case 178: case 192: case 193: case 194: case 195: case 196: case 197: case 198: case 199: case 200: case 201: case 206: {

#line  2674 "VBNET.ATG" 
			Expression val = null;
			AssignmentOperatorType op;
			
			bool mustBeAssignment = la.kind == Tokens.Plus  || la.kind == Tokens.Minus ||
			                        la.kind == Tokens.Not   || la.kind == Tokens.Times;
			
			SimpleExpr(
#line  2680 "VBNET.ATG" 
out expr);
			if (StartOf(39)) {
				AssignmentOperator(
#line  2682 "VBNET.ATG" 
out op);
				Expr(
#line  2682 "VBNET.ATG" 
out val);

#line  2682 "VBNET.ATG" 
				expr = new AssignmentExpression(expr, op, val); 
			} else if (la.kind == 1 || la.kind == 13 || la.kind == 87) {

#line  2683 "VBNET.ATG" 
				if (mustBeAssignment) Error("error in assignment."); 
			} else SynErr(260);

#line  2686 "VBNET.ATG" 
			// a field reference expression that stands alone is a
			// invocation expression without parantheses and arguments
			if(expr is MemberReferenceExpression || expr is IdentifierExpression) {
				expr = new InvocationExpression(expr);
			}
			statement = new ExpressionStatement(expr);
			
			break;
		}
		case 57: {
			lexer.NextToken();
			SimpleExpr(
#line  2693 "VBNET.ATG" 
out expr);

#line  2693 "VBNET.ATG" 
			statement = new ExpressionStatement(expr); 
			break;
		}
		case 190: {
			lexer.NextToken();

#line  2695 "VBNET.ATG" 
			Statement block;  
			if (
#line  2696 "VBNET.ATG" 
Peek(1).kind == Tokens.As) {

#line  2697 "VBNET.ATG" 
				LocalVariableDeclaration resourceAquisition = new LocalVariableDeclaration(Modifiers.None); 
				VariableDeclarator(
#line  2698 "VBNET.ATG" 
resourceAquisition.Variables);
				while (la.kind == 12) {
					lexer.NextToken();
					VariableDeclarator(
#line  2700 "VBNET.ATG" 
resourceAquisition.Variables);
				}
				Block(
#line  2702 "VBNET.ATG" 
out block);

#line  2704 "VBNET.ATG" 
				statement = new UsingStatement(resourceAquisition, block);
				SetParent(resourceAquisition.Variables, resourceAquisition);
				
			} else if (StartOf(27)) {
				Expr(
#line  2707 "VBNET.ATG" 
out expr);
				Block(
#line  2708 "VBNET.ATG" 
out block);

#line  2709 "VBNET.ATG" 
				statement = new UsingStatement(new ExpressionStatement(expr), block); 
			} else SynErr(261);
			Expect(89);
			Expect(190);
			break;
		}
		default: SynErr(262); break;
		}
	}

	void LocalDeclarationStatement(
#line  2400 "VBNET.ATG" 
out Statement statement) {

#line  2402 "VBNET.ATG" 
		ModifierList m = new ModifierList();
		LocalVariableDeclaration localVariableDeclaration;
		bool dimfound = false;
		
		while (la.kind == 72 || la.kind == 82 || la.kind == 162) {
			if (la.kind == 72) {
				lexer.NextToken();

#line  2408 "VBNET.ATG" 
				m.Add(Modifiers.Const, t.Location); 
			} else if (la.kind == 162) {
				lexer.NextToken();

#line  2409 "VBNET.ATG" 
				m.Add(Modifiers.Static, t.Location); 
			} else {
				lexer.NextToken();

#line  2410 "VBNET.ATG" 
				dimfound = true; 
			}
		}

#line  2413 "VBNET.ATG" 
		if(dimfound && (m.Modifier & Modifiers.Const) != 0) {
		Error("Dim is not allowed on constants.");
		}
		
		if(m.isNone && dimfound == false) {
			Error("Const, Dim or Static expected");
		}
		
		localVariableDeclaration = new LocalVariableDeclaration(m.Modifier);
		localVariableDeclaration.StartLocation = t.Location;
		
		VariableDeclarator(
#line  2424 "VBNET.ATG" 
localVariableDeclaration.Variables);
		while (la.kind == 12) {
			lexer.NextToken();
			VariableDeclarator(
#line  2425 "VBNET.ATG" 
localVariableDeclaration.Variables);
		}

#line  2427 "VBNET.ATG" 
		SetParent(localVariableDeclaration.Variables, localVariableDeclaration);
		statement = localVariableDeclaration;
		
	}

	void TryStatement(
#line  2913 "VBNET.ATG" 
out Statement tryStatement) {

#line  2915 "VBNET.ATG" 
		Statement blockStmt = null, finallyStmt = null;List<CatchClause> catchClauses = null;
		
		Expect(175);
		EndOfStmt();
		Block(
#line  2918 "VBNET.ATG" 
out blockStmt);
		if (la.kind == 59 || la.kind == 89 || la.kind == 98) {
			CatchClauses(
#line  2919 "VBNET.ATG" 
out catchClauses);
		}
		if (la.kind == 98) {
			lexer.NextToken();
			EndOfStmt();
			Block(
#line  2920 "VBNET.ATG" 
out finallyStmt);
		}
		Expect(89);
		Expect(175);

#line  2923 "VBNET.ATG" 
		tryStatement = new TryCatchStatement(blockStmt, catchClauses, finallyStmt);
		
	}

	void WithStatement(
#line  2893 "VBNET.ATG" 
out Statement withStatement) {

#line  2895 "VBNET.ATG" 
		Statement blockStmt = null;
		Expression expr = null;
		
		Expect(183);

#line  2898 "VBNET.ATG" 
		Location start = t.Location; 
		Expr(
#line  2899 "VBNET.ATG" 
out expr);
		EndOfStmt();

#line  2901 "VBNET.ATG" 
		withStatement = new WithStatement(expr);
		withStatement.StartLocation = start;
		
		Block(
#line  2904 "VBNET.ATG" 
out blockStmt);

#line  2906 "VBNET.ATG" 
		((WithStatement)withStatement).Body = (BlockStatement)blockStmt;
		
		Expect(89);
		Expect(183);

#line  2909 "VBNET.ATG" 
		withStatement.EndLocation = t.Location; 
	}

	void WhileOrUntil(
#line  2886 "VBNET.ATG" 
out ConditionType conditionType) {

#line  2887 "VBNET.ATG" 
		conditionType = ConditionType.None; 
		if (la.kind == 182) {
			lexer.NextToken();

#line  2888 "VBNET.ATG" 
			conditionType = ConditionType.While; 
		} else if (la.kind == 178) {
			lexer.NextToken();

#line  2889 "VBNET.ATG" 
			conditionType = ConditionType.Until; 
		} else SynErr(263);
	}

	void LoopControlVariable(
#line  2728 "VBNET.ATG" 
out TypeReference type, out string name) {

#line  2729 "VBNET.ATG" 
		ArrayList arrayModifiers = null;
		type = null;
		
		Qualident(
#line  2733 "VBNET.ATG" 
out name);
		if (
#line  2734 "VBNET.ATG" 
IsDims()) {
			ArrayTypeModifiers(
#line  2734 "VBNET.ATG" 
out arrayModifiers);
		}
		if (la.kind == 49) {
			lexer.NextToken();
			TypeName(
#line  2735 "VBNET.ATG" 
out type);

#line  2735 "VBNET.ATG" 
			if (name.IndexOf('.') > 0) { Error("No type def for 'for each' member indexer allowed."); } 
		}

#line  2737 "VBNET.ATG" 
		if (type != null) {
		if(type.RankSpecifier != null && arrayModifiers != null) {
			Error("array rank only allowed one time");
		} else if (arrayModifiers != null) {
			type.RankSpecifier = (int[])arrayModifiers.ToArray(typeof(int));
		}
		}
		
	}

	void ReDimClause(
#line  2807 "VBNET.ATG" 
out Expression expr) {
		SimpleNonInvocationExpression(
#line  2809 "VBNET.ATG" 
out expr);
		ReDimClauseInternal(
#line  2810 "VBNET.ATG" 
ref expr);
	}

	void SingleLineStatementList(
#line  2714 "VBNET.ATG" 
List<Statement> list) {

#line  2715 "VBNET.ATG" 
		Statement embeddedStatement = null; 
		if (la.kind == 89) {
			lexer.NextToken();

#line  2717 "VBNET.ATG" 
			embeddedStatement = new EndStatement(); 
		} else if (StartOf(36)) {
			EmbeddedStatement(
#line  2718 "VBNET.ATG" 
out embeddedStatement);
		} else SynErr(264);

#line  2719 "VBNET.ATG" 
		if (embeddedStatement != null) list.Add(embeddedStatement); 
		while (la.kind == 13) {
			lexer.NextToken();
			while (la.kind == 13) {
				lexer.NextToken();
			}
			if (la.kind == 89) {
				lexer.NextToken();

#line  2721 "VBNET.ATG" 
				embeddedStatement = new EndStatement(); 
			} else if (StartOf(36)) {
				EmbeddedStatement(
#line  2722 "VBNET.ATG" 
out embeddedStatement);
			} else SynErr(265);

#line  2723 "VBNET.ATG" 
			if (embeddedStatement != null) list.Add(embeddedStatement); 
		}
	}

	void CaseClauses(
#line  2846 "VBNET.ATG" 
out List<CaseLabel> caseClauses) {

#line  2848 "VBNET.ATG" 
		caseClauses = new List<CaseLabel>();
		CaseLabel caseClause = null;
		
		CaseClause(
#line  2851 "VBNET.ATG" 
out caseClause);

#line  2851 "VBNET.ATG" 
		if (caseClause != null) { caseClauses.Add(caseClause); } 
		while (la.kind == 12) {
			lexer.NextToken();
			CaseClause(
#line  2852 "VBNET.ATG" 
out caseClause);

#line  2852 "VBNET.ATG" 
			if (caseClause != null) { caseClauses.Add(caseClause); } 
		}
	}

	void OnErrorStatement(
#line  2748 "VBNET.ATG" 
out OnErrorStatement stmt) {

#line  2750 "VBNET.ATG" 
		stmt = null;
		GotoStatement goToStatement = null;
		
		Expect(136);
		Expect(93);
		if (
#line  2756 "VBNET.ATG" 
IsNegativeLabelName()) {
			Expect(105);
			Expect(15);
			Expect(5);

#line  2758 "VBNET.ATG" 
			long intLabel = Int64.Parse(t.val);
			if(intLabel != 1) {
				Error("invalid label in on error statement.");
			}
			stmt = new OnErrorStatement(new GotoStatement((intLabel * -1).ToString()));
			
		} else if (la.kind == 105) {
			GotoStatement(
#line  2764 "VBNET.ATG" 
out goToStatement);

#line  2766 "VBNET.ATG" 
			string val = goToStatement.Label;
			
			// if value is numeric, make sure that is 0
			try {
				long intLabel = Int64.Parse(val);
				if(intLabel != 0) {
					Error("invalid label in on error statement.");
				}
			} catch {
			}
			stmt = new OnErrorStatement(goToStatement);
			
		} else if (la.kind == 154) {
			lexer.NextToken();
			Expect(129);

#line  2780 "VBNET.ATG" 
			stmt = new OnErrorStatement(new ResumeStatement(true));
			
		} else SynErr(266);
	}

	void GotoStatement(
#line  2786 "VBNET.ATG" 
out GotoStatement goToStatement) {

#line  2788 "VBNET.ATG" 
		string label = String.Empty;
		
		Expect(105);
		LabelName(
#line  2791 "VBNET.ATG" 
out label);

#line  2793 "VBNET.ATG" 
		goToStatement = new GotoStatement(label);
		
	}

	void ResumeStatement(
#line  2835 "VBNET.ATG" 
out ResumeStatement resumeStatement) {

#line  2837 "VBNET.ATG" 
		resumeStatement = null;
		string label = String.Empty;
		
		if (
#line  2840 "VBNET.ATG" 
IsResumeNext()) {
			Expect(154);
			Expect(129);

#line  2841 "VBNET.ATG" 
			resumeStatement = new ResumeStatement(true); 
		} else if (la.kind == 154) {
			lexer.NextToken();
			if (StartOf(40)) {
				LabelName(
#line  2842 "VBNET.ATG" 
out label);
			}

#line  2842 "VBNET.ATG" 
			resumeStatement = new ResumeStatement(label); 
		} else SynErr(267);
	}

	void ReDimClauseInternal(
#line  2813 "VBNET.ATG" 
ref Expression expr) {

#line  2814 "VBNET.ATG" 
		List<Expression> arguments; bool canBeNormal; bool canBeRedim; string name; 
		while (la.kind == 10 || 
#line  2817 "VBNET.ATG" 
la.kind == Tokens.OpenParenthesis && Peek(1).kind == Tokens.Of) {
			if (la.kind == 10) {
				lexer.NextToken();
				IdentifierOrKeyword(
#line  2816 "VBNET.ATG" 
out name);

#line  2816 "VBNET.ATG" 
				expr = new MemberReferenceExpression(expr, name); 
			} else {
				InvocationExpression(
#line  2818 "VBNET.ATG" 
ref expr);
			}
		}
		Expect(25);
		NormalOrReDimArgumentList(
#line  2821 "VBNET.ATG" 
out arguments, out canBeNormal, out canBeRedim);
		Expect(26);

#line  2823 "VBNET.ATG" 
		expr = new InvocationExpression(expr, arguments);
		SetParent(arguments, expr);
		if (canBeRedim == false || canBeNormal && (la.kind == Tokens.Dot || la.kind == Tokens.OpenParenthesis)) {
			if (this.Errors.Count == 0) {
				// don't recurse on parse errors - could result in endless recursion
				ReDimClauseInternal(ref expr);
			}
		}
		
	}

	void CaseClause(
#line  2856 "VBNET.ATG" 
out CaseLabel caseClause) {

#line  2858 "VBNET.ATG" 
		Expression expr = null;
		Expression sexpr = null;
		BinaryOperatorType op = BinaryOperatorType.None;
		caseClause = null;
		
		if (la.kind == 87) {
			lexer.NextToken();

#line  2864 "VBNET.ATG" 
			caseClause = new CaseLabel(); 
		} else if (StartOf(41)) {
			if (la.kind == 114) {
				lexer.NextToken();
			}
			switch (la.kind) {
			case 28: {
				lexer.NextToken();

#line  2868 "VBNET.ATG" 
				op = BinaryOperatorType.LessThan; 
				break;
			}
			case 27: {
				lexer.NextToken();

#line  2869 "VBNET.ATG" 
				op = BinaryOperatorType.GreaterThan; 
				break;
			}
			case 31: {
				lexer.NextToken();

#line  2870 "VBNET.ATG" 
				op = BinaryOperatorType.LessThanOrEqual; 
				break;
			}
			case 30: {
				lexer.NextToken();

#line  2871 "VBNET.ATG" 
				op = BinaryOperatorType.GreaterThanOrEqual; 
				break;
			}
			case 11: {
				lexer.NextToken();

#line  2872 "VBNET.ATG" 
				op = BinaryOperatorType.Equality; 
				break;
			}
			case 29: {
				lexer.NextToken();

#line  2873 "VBNET.ATG" 
				op = BinaryOperatorType.InEquality; 
				break;
			}
			default: SynErr(268); break;
			}
			Expr(
#line  2875 "VBNET.ATG" 
out expr);

#line  2877 "VBNET.ATG" 
			caseClause = new CaseLabel(op, expr);
			
		} else if (StartOf(27)) {
			Expr(
#line  2879 "VBNET.ATG" 
out expr);
			if (la.kind == 173) {
				lexer.NextToken();
				Expr(
#line  2879 "VBNET.ATG" 
out sexpr);
			}

#line  2881 "VBNET.ATG" 
			caseClause = new CaseLabel(expr, sexpr);
			
		} else SynErr(269);
	}

	void CatchClauses(
#line  2928 "VBNET.ATG" 
out List<CatchClause> catchClauses) {

#line  2930 "VBNET.ATG" 
		catchClauses = new List<CatchClause>();
		TypeReference type = null;
		Statement blockStmt = null;
		Expression expr = null;
		string name = String.Empty;
		
		while (la.kind == 59) {
			lexer.NextToken();
			if (StartOf(13)) {
				Identifier();

#line  2938 "VBNET.ATG" 
				name = t.val; 
				if (la.kind == 49) {
					lexer.NextToken();
					TypeName(
#line  2938 "VBNET.ATG" 
out type);
				}
			}
			if (la.kind == 181) {
				lexer.NextToken();
				Expr(
#line  2939 "VBNET.ATG" 
out expr);
			}
			EndOfStmt();
			Block(
#line  2941 "VBNET.ATG" 
out blockStmt);

#line  2942 "VBNET.ATG" 
			catchClauses.Add(new CatchClause(type, name, blockStmt, expr)); 
		}
	}


	
	public override void Parse()
	{
		VBNET();

	}
	
	protected override void SynErr(int line, int col, int errorNumber)
	{
		string s;
		switch (errorNumber) {
			case 0: s = "EOF expected"; break;
			case 1: s = "EOL expected"; break;
			case 2: s = "ident expected"; break;
			case 3: s = "LiteralString expected"; break;
			case 4: s = "LiteralCharacter expected"; break;
			case 5: s = "LiteralInteger expected"; break;
			case 6: s = "LiteralDouble expected"; break;
			case 7: s = "LiteralSingle expected"; break;
			case 8: s = "LiteralDecimal expected"; break;
			case 9: s = "LiteralDate expected"; break;
			case 10: s = "\".\" expected"; break;
			case 11: s = "\"=\" expected"; break;
			case 12: s = "\",\" expected"; break;
			case 13: s = "\":\" expected"; break;
			case 14: s = "\"+\" expected"; break;
			case 15: s = "\"-\" expected"; break;
			case 16: s = "\"*\" expected"; break;
			case 17: s = "\"/\" expected"; break;
			case 18: s = "\"\\\\\" expected"; break;
			case 19: s = "\"&\" expected"; break;
			case 20: s = "\"^\" expected"; break;
			case 21: s = "\"?\" expected"; break;
			case 22: s = "\"!\" expected"; break;
			case 23: s = "\"{\" expected"; break;
			case 24: s = "\"}\" expected"; break;
			case 25: s = "\"(\" expected"; break;
			case 26: s = "\")\" expected"; break;
			case 27: s = "\">\" expected"; break;
			case 28: s = "\"<\" expected"; break;
			case 29: s = "\"<>\" expected"; break;
			case 30: s = "\">=\" expected"; break;
			case 31: s = "\"<=\" expected"; break;
			case 32: s = "\"<<\" expected"; break;
			case 33: s = "\">>\" expected"; break;
			case 34: s = "\"+=\" expected"; break;
			case 35: s = "\"^=\" expected"; break;
			case 36: s = "\"-=\" expected"; break;
			case 37: s = "\"*=\" expected"; break;
			case 38: s = "\"/=\" expected"; break;
			case 39: s = "\"\\\\=\" expected"; break;
			case 40: s = "\"<<=\" expected"; break;
			case 41: s = "\">>=\" expected"; break;
			case 42: s = "\"&=\" expected"; break;
			case 43: s = "\"AddHandler\" expected"; break;
			case 44: s = "\"AddressOf\" expected"; break;
			case 45: s = "\"Alias\" expected"; break;
			case 46: s = "\"And\" expected"; break;
			case 47: s = "\"AndAlso\" expected"; break;
			case 48: s = "\"Ansi\" expected"; break;
			case 49: s = "\"As\" expected"; break;
			case 50: s = "\"Assembly\" expected"; break;
			case 51: s = "\"Auto\" expected"; break;
			case 52: s = "\"Binary\" expected"; break;
			case 53: s = "\"Boolean\" expected"; break;
			case 54: s = "\"ByRef\" expected"; break;
			case 55: s = "\"Byte\" expected"; break;
			case 56: s = "\"ByVal\" expected"; break;
			case 57: s = "\"Call\" expected"; break;
			case 58: s = "\"Case\" expected"; break;
			case 59: s = "\"Catch\" expected"; break;
			case 60: s = "\"CBool\" expected"; break;
			case 61: s = "\"CByte\" expected"; break;
			case 62: s = "\"CChar\" expected"; break;
			case 63: s = "\"CDate\" expected"; break;
			case 64: s = "\"CDbl\" expected"; break;
			case 65: s = "\"CDec\" expected"; break;
			case 66: s = "\"Char\" expected"; break;
			case 67: s = "\"CInt\" expected"; break;
			case 68: s = "\"Class\" expected"; break;
			case 69: s = "\"CLng\" expected"; break;
			case 70: s = "\"CObj\" expected"; break;
			case 71: s = "\"Compare\" expected"; break;
			case 72: s = "\"Const\" expected"; break;
			case 73: s = "\"CShort\" expected"; break;
			case 74: s = "\"CSng\" expected"; break;
			case 75: s = "\"CStr\" expected"; break;
			case 76: s = "\"CType\" expected"; break;
			case 77: s = "\"Date\" expected"; break;
			case 78: s = "\"Decimal\" expected"; break;
			case 79: s = "\"Declare\" expected"; break;
			case 80: s = "\"Default\" expected"; break;
			case 81: s = "\"Delegate\" expected"; break;
			case 82: s = "\"Dim\" expected"; break;
			case 83: s = "\"DirectCast\" expected"; break;
			case 84: s = "\"Do\" expected"; break;
			case 85: s = "\"Double\" expected"; break;
			case 86: s = "\"Each\" expected"; break;
			case 87: s = "\"Else\" expected"; break;
			case 88: s = "\"ElseIf\" expected"; break;
			case 89: s = "\"End\" expected"; break;
			case 90: s = "\"EndIf\" expected"; break;
			case 91: s = "\"Enum\" expected"; break;
			case 92: s = "\"Erase\" expected"; break;
			case 93: s = "\"Error\" expected"; break;
			case 94: s = "\"Event\" expected"; break;
			case 95: s = "\"Exit\" expected"; break;
			case 96: s = "\"Explicit\" expected"; break;
			case 97: s = "\"False\" expected"; break;
			case 98: s = "\"Finally\" expected"; break;
			case 99: s = "\"For\" expected"; break;
			case 100: s = "\"Friend\" expected"; break;
			case 101: s = "\"Function\" expected"; break;
			case 102: s = "\"Get\" expected"; break;
			case 103: s = "\"GetType\" expected"; break;
			case 104: s = "\"GoSub\" expected"; break;
			case 105: s = "\"GoTo\" expected"; break;
			case 106: s = "\"Handles\" expected"; break;
			case 107: s = "\"If\" expected"; break;
			case 108: s = "\"Implements\" expected"; break;
			case 109: s = "\"Imports\" expected"; break;
			case 110: s = "\"In\" expected"; break;
			case 111: s = "\"Inherits\" expected"; break;
			case 112: s = "\"Integer\" expected"; break;
			case 113: s = "\"Interface\" expected"; break;
			case 114: s = "\"Is\" expected"; break;
			case 115: s = "\"Let\" expected"; break;
			case 116: s = "\"Lib\" expected"; break;
			case 117: s = "\"Like\" expected"; break;
			case 118: s = "\"Long\" expected"; break;
			case 119: s = "\"Loop\" expected"; break;
			case 120: s = "\"Me\" expected"; break;
			case 121: s = "\"Mod\" expected"; break;
			case 122: s = "\"Module\" expected"; break;
			case 123: s = "\"MustInherit\" expected"; break;
			case 124: s = "\"MustOverride\" expected"; break;
			case 125: s = "\"MyBase\" expected"; break;
			case 126: s = "\"MyClass\" expected"; break;
			case 127: s = "\"Namespace\" expected"; break;
			case 128: s = "\"New\" expected"; break;
			case 129: s = "\"Next\" expected"; break;
			case 130: s = "\"Not\" expected"; break;
			case 131: s = "\"Nothing\" expected"; break;
			case 132: s = "\"NotInheritable\" expected"; break;
			case 133: s = "\"NotOverridable\" expected"; break;
			case 134: s = "\"Object\" expected"; break;
			case 135: s = "\"Off\" expected"; break;
			case 136: s = "\"On\" expected"; break;
			case 137: s = "\"Option\" expected"; break;
			case 138: s = "\"Optional\" expected"; break;
			case 139: s = "\"Or\" expected"; break;
			case 140: s = "\"OrElse\" expected"; break;
			case 141: s = "\"Overloads\" expected"; break;
			case 142: s = "\"Overridable\" expected"; break;
			case 143: s = "\"Overrides\" expected"; break;
			case 144: s = "\"ParamArray\" expected"; break;
			case 145: s = "\"Preserve\" expected"; break;
			case 146: s = "\"Private\" expected"; break;
			case 147: s = "\"Property\" expected"; break;
			case 148: s = "\"Protected\" expected"; break;
			case 149: s = "\"Public\" expected"; break;
			case 150: s = "\"RaiseEvent\" expected"; break;
			case 151: s = "\"ReadOnly\" expected"; break;
			case 152: s = "\"ReDim\" expected"; break;
			case 153: s = "\"RemoveHandler\" expected"; break;
			case 154: s = "\"Resume\" expected"; break;
			case 155: s = "\"Return\" expected"; break;
			case 156: s = "\"Select\" expected"; break;
			case 157: s = "\"Set\" expected"; break;
			case 158: s = "\"Shadows\" expected"; break;
			case 159: s = "\"Shared\" expected"; break;
			case 160: s = "\"Short\" expected"; break;
			case 161: s = "\"Single\" expected"; break;
			case 162: s = "\"Static\" expected"; break;
			case 163: s = "\"Step\" expected"; break;
			case 164: s = "\"Stop\" expected"; break;
			case 165: s = "\"Strict\" expected"; break;
			case 166: s = "\"String\" expected"; break;
			case 167: s = "\"Structure\" expected"; break;
			case 168: s = "\"Sub\" expected"; break;
			case 169: s = "\"SyncLock\" expected"; break;
			case 170: s = "\"Text\" expected"; break;
			case 171: s = "\"Then\" expected"; break;
			case 172: s = "\"Throw\" expected"; break;
			case 173: s = "\"To\" expected"; break;
			case 174: s = "\"True\" expected"; break;
			case 175: s = "\"Try\" expected"; break;
			case 176: s = "\"TypeOf\" expected"; break;
			case 177: s = "\"Unicode\" expected"; break;
			case 178: s = "\"Until\" expected"; break;
			case 179: s = "\"Variant\" expected"; break;
			case 180: s = "\"Wend\" expected"; break;
			case 181: s = "\"When\" expected"; break;
			case 182: s = "\"While\" expected"; break;
			case 183: s = "\"With\" expected"; break;
			case 184: s = "\"WithEvents\" expected"; break;
			case 185: s = "\"WriteOnly\" expected"; break;
			case 186: s = "\"Xor\" expected"; break;
			case 187: s = "\"Rem\" expected"; break;
			case 188: s = "\"Continue\" expected"; break;
			case 189: s = "\"Operator\" expected"; break;
			case 190: s = "\"Using\" expected"; break;
			case 191: s = "\"IsNot\" expected"; break;
			case 192: s = "\"SByte\" expected"; break;
			case 193: s = "\"UInteger\" expected"; break;
			case 194: s = "\"ULong\" expected"; break;
			case 195: s = "\"UShort\" expected"; break;
			case 196: s = "\"CSByte\" expected"; break;
			case 197: s = "\"CUShort\" expected"; break;
			case 198: s = "\"CUInt\" expected"; break;
			case 199: s = "\"CULng\" expected"; break;
			case 200: s = "\"Global\" expected"; break;
			case 201: s = "\"TryCast\" expected"; break;
			case 202: s = "\"Of\" expected"; break;
			case 203: s = "\"Narrowing\" expected"; break;
			case 204: s = "\"Widening\" expected"; break;
			case 205: s = "\"Partial\" expected"; break;
			case 206: s = "\"Custom\" expected"; break;
			case 207: s = "??? expected"; break;
			case 208: s = "invalid EndOfStmt"; break;
			case 209: s = "invalid OptionStmt"; break;
			case 210: s = "invalid OptionStmt"; break;
			case 211: s = "invalid GlobalAttributeSection"; break;
			case 212: s = "invalid GlobalAttributeSection"; break;
			case 213: s = "invalid NamespaceMemberDecl"; break;
			case 214: s = "invalid OptionValue"; break;
			case 215: s = "invalid TypeModifier"; break;
			case 216: s = "invalid NonModuleDeclaration"; break;
			case 217: s = "invalid NonModuleDeclaration"; break;
			case 218: s = "invalid Identifier"; break;
			case 219: s = "invalid TypeParameterConstraints"; break;
			case 220: s = "invalid TypeParameterConstraint"; break;
			case 221: s = "invalid NonArrayTypeName"; break;
			case 222: s = "invalid MemberModifier"; break;
			case 223: s = "invalid StructureMemberDecl"; break;
			case 224: s = "invalid StructureMemberDecl"; break;
			case 225: s = "invalid StructureMemberDecl"; break;
			case 226: s = "invalid StructureMemberDecl"; break;
			case 227: s = "invalid StructureMemberDecl"; break;
			case 228: s = "invalid StructureMemberDecl"; break;
			case 229: s = "invalid StructureMemberDecl"; break;
			case 230: s = "invalid InterfaceMemberDecl"; break;
			case 231: s = "invalid InterfaceMemberDecl"; break;
			case 232: s = "invalid Charset"; break;
			case 233: s = "invalid IdentifierForFieldDeclaration"; break;
			case 234: s = "invalid VariableDeclaratorPartAfterIdentifier"; break;
			case 235: s = "invalid AccessorDecls"; break;
			case 236: s = "invalid EventAccessorDeclaration"; break;
			case 237: s = "invalid OverloadableOperator"; break;
			case 238: s = "invalid VariableInitializer"; break;
			case 239: s = "invalid EventMemberSpecifier"; break;
			case 240: s = "invalid AssignmentOperator"; break;
			case 241: s = "invalid SimpleNonInvocationExpression"; break;
			case 242: s = "invalid SimpleNonInvocationExpression"; break;
			case 243: s = "invalid SimpleNonInvocationExpression"; break;
			case 244: s = "invalid SimpleNonInvocationExpression"; break;
			case 245: s = "invalid InvocationExpression"; break;
			case 246: s = "invalid InvocationExpression"; break;
			case 247: s = "invalid PrimitiveTypeName"; break;
			case 248: s = "invalid CastTarget"; break;
			case 249: s = "invalid ComparisonExpr"; break;
			case 250: s = "invalid Argument"; break;
			case 251: s = "invalid QualIdentAndTypeArguments"; break;
			case 252: s = "invalid AttributeArguments"; break;
			case 253: s = "invalid ParameterModifier"; break;
			case 254: s = "invalid Statement"; break;
			case 255: s = "invalid LabelName"; break;
			case 256: s = "invalid EmbeddedStatement"; break;
			case 257: s = "invalid EmbeddedStatement"; break;
			case 258: s = "invalid EmbeddedStatement"; break;
			case 259: s = "invalid EmbeddedStatement"; break;
			case 260: s = "invalid EmbeddedStatement"; break;
			case 261: s = "invalid EmbeddedStatement"; break;
			case 262: s = "invalid EmbeddedStatement"; break;
			case 263: s = "invalid WhileOrUntil"; break;
			case 264: s = "invalid SingleLineStatementList"; break;
			case 265: s = "invalid SingleLineStatementList"; break;
			case 266: s = "invalid OnErrorStatement"; break;
			case 267: s = "invalid ResumeStatement"; break;
			case 268: s = "invalid CaseClause"; break;
			case 269: s = "invalid CaseClause"; break;

			default: s = "error " + errorNumber; break;
		}
		this.Errors.Error(line, col, s);
	}
	
	private bool StartOf(int s)
	{
		return set[s, lexer.LookAhead.kind];
	}
	
	static bool[,] set = {
	{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,T,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,T,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, x,x,x,x, x,x,T,x, T,x,x,T, x,x,x,x, x,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,T, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, T,x,x,x, x,x,T,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,x, x,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, T,x,x,x, x,x,T,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,T, T,x,x,x, x,x,x,T, T,T,T,x, x,x,x,x, x,x,x,T, x,x,T,x, T,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, T,x,x,x, x,x,x,x, T,T,x,T, x,x,x,x, x,T,T,T, x,T,T,T, T,T,x,T, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,T, T,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, T,T,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,T,T,T, x,x,T,x, T,T,x,T, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,T, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, T,T,T,x, x,x,x,x, x,x,x,T, x,x,T,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, T,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,T,T,T, x,x,T,T, T,T,x,T, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,T,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,T,T,T, x,x,T,T, T,T,x,T, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,T,T,T, T,T,T,T, T,T,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, T,x,T,T, T,T,x,T, x,T,x,x, T,T,T,T, T,T,T,T, x,T,T,T, T,T,T,T, T,T,T,x, x,x,T,T, T,T,x,x, x,x,x,x, T,T,x,T, T,T,x,T, x,x,x,T, x,T,x,T, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,x,T, x,x,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,T,x, T,T,T,T, T,x,x,x, T,T,T,x, T,x,T,x, x,T,T,x, T,x,T,T, T,T,T,x, x,x,T,T, x,x,x,x, T,x,T,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,T,T,T, T,T,T,T, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, T,T,T,T, T,T,x,T, x,T,x,x, T,T,T,T, T,T,T,T, x,T,T,T, T,T,T,T, T,T,T,x, x,x,T,T, T,T,x,x, x,T,x,x, T,T,x,T, T,T,x,T, x,x,x,T, x,T,x,T, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,x,T, x,x,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,T,x, T,T,T,T, T,x,x,x, T,T,T,x, T,x,T,x, x,T,T,x, T,x,T,T, T,T,T,x, x,x,T,T, x,x,x,x, T,x,T,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,T,T, T,T,T,T, T,T,T,x, x,x,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, x,T,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,T,T, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,T,x, x,x,T,x, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,T,T, T,T,T,T, T,T,T,x, x,x,T,T, T,x,x,x, x,x,x,T, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, x,T,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,T,T, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,T,x, x,x,T,x, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,T,T, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, x,T,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,x,T, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,T,x, x,x,T,x, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,T, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,x,T, x,T,T,x, x,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, x,x,x,x, x},
	{x,x,T,T, T,T,T,T, T,T,T,x, T,x,T,T, T,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, x,T,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,T,T, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,T,x, x,x,T,x, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,T,T, T,T,T,T, T,T,T,x, x,x,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,T,T, T,T,x,T, x,x,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, x,T,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,x,T, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,T,x, x,x,T,x, x,x,T,x, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, T,x,T,T, T,T,x,T, x,T,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, T,T,x,x, x,x,x,x, T,T,x,T, T,T,x,T, x,x,x,T, x,T,x,T, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,x,T, x,x,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,T,x, T,T,T,T, T,x,x,x, T,T,x,x, T,x,T,x, x,T,T,x, T,x,T,T, T,T,T,x, x,x,T,T, x,x,x,x, T,x,T,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x},
	{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, T,x,T,T, T,T,x,T, x,T,x,x, T,T,T,T, T,T,T,T, x,T,T,T, x,T,T,T, T,T,T,x, x,x,x,T, T,T,x,x, x,T,x,x, T,T,x,T, T,T,x,T, x,x,x,T, x,T,x,T, x,x,x,x, T,x,x,x, x,x,T,x, T,x,x,x, x,T,T,x, T,x,x,T, x,x,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,T,x, T,T,T,T, T,x,x,x, T,T,x,x, T,x,T,x, x,T,T,x, T,x,T,T, T,T,T,x, x,x,T,T, x,x,x,x, T,x,T,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
	{x,x,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x},
	{x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x}

	};
} // end Parser

}