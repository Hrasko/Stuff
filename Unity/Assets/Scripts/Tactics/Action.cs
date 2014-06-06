namespace Tactics
{
    [System.Serializable]
    public class Action : System.Object
    {
        ActionTemplate template;
        bool used = false;
		public string name { get { return template.name(); } }
		public void use(Character actor)
		{
			used = true;
			template.activate (actor);
		}
    }
}