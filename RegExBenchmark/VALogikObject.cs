namespace RegExBenchmark
{
	internal class VALogikObject : IVaLogikObject
	{
		public string Identifier { get; set; }
		public ushort Nr { get; set; }

		public VALogikObject(string identifier, ushort nr)
		{
			Identifier = identifier;
			Nr = nr;
		}
	}

}
