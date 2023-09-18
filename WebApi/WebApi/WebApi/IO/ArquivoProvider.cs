namespace WebApi.IO
{
    public static class ArquivoProvider
    {
        public static string ObterLinkImagemLogotipo(string IdLogotipo)
        {
            return $"link_{IdLogotipo}";
        }
    }
}
