namespace OrientDb
{
	public class CommandOptions
    {
		public enum CommandLanguage
		{
			Sql,
			Gremlin
		}

		public CommandLanguage Language { get; set; } = CommandLanguage.Sql;
		public int Limit { get; set; } = 20;
		public FetchPlan FetchPlan { get; set; } = new FetchPlan();
	}
}
