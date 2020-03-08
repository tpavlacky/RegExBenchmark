namespace RegExBenchmark
{
	internal interface IObjectProvider
	{
		IVaLogikObject Get(string objectName, string identifier);

		bool TryGetObject(string objectName, string idenfitier, out IVaLogikObject obj);
	}

}
