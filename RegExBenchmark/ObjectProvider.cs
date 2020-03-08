namespace RegExBenchmark
{
	internal class ObjectProvider : IObjectProvider
	{
		public IVaLogikObject Get(string objectName, string identifier)
		{
			return new VALogikObject("Test123", 32);
		}

		public bool TryGetObject(string objectName, string idenfitier, out IVaLogikObject obj)
		{
			obj = new VALogikObject("Test123", 42);
			return true;
		}
	}

}
