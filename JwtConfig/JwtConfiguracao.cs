namespace LojaManoelApi.JwtConfig
{
    public class JwtConfiguracao
    {
        public string Secret { get; set; }

        public string Expires { get; set; }
        public string Sender { get; set; }
        public string Audience { get; set; }
    }
}
