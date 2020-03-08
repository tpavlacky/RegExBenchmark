using System.Linq;
using System.Text.RegularExpressions;

namespace RegExBenchmark
{
	internal class ScalaObjectIdentifierReplacer
	{
		private const string OBJECT_NAME_PATTERN = "\\w*";
		private const string IDENTIFIER_PATTERN = "[0-9a-f]{8}[-]?(?:[0-9a-f]{4}[-]?){3}[0-9a-f]{12}";
		private readonly string PATTERN = $@"{{({OBJECT_NAME_PATTERN}):({IDENTIFIER_PATTERN})}}";
		private Regex _preCompiledRegex;


		private Regex Regex
		{
			get
			{
				if(_preCompiledRegex == null)
				{
					_preCompiledRegex = new Regex(PATTERN, RegexOptions.Compiled);
				}

				return _preCompiledRegex;
			}
		}

		private readonly IObjectProvider _objectProvider;

		public ScalaObjectIdentifierReplacer(IObjectProvider objectProvider)
		{
			_objectProvider = objectProvider;

		}

		public string Replace(string input)
		{
			var matches = Regex.Matches(input);
			//var matches = Regex.Matches(input, PATTERN);

			if (matches.Count == 0)
			{
				return input;
			}

			var sb = StringBuilderCache.Acquire();
			sb.Append(input);
			foreach (var match in matches.Cast<Match>().Reverse())
			{
				var objectName = match.Groups[1].Value;
				var identifier = match.Groups[2].Value;

				if (!_objectProvider.TryGetObject(objectName, identifier, out IVaLogikObject obj))
				{
					return "";
				}

				var newValue = "NewValue:" + obj.Nr;
				sb.Remove(match.Index, match.Length);
				sb.Insert(match.Index, newValue);
			}

			return StringBuilderCache.GetStringAndRelease(sb);
		}
	}

}
