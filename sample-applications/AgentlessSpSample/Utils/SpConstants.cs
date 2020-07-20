static class SpConstants
{
    // endpoints
    public const string PICKUP_ENDPOINT = "/ext/ref/pickup";
    public const string SP_SSO_ENDPOINT = "/sp/startSSO.ping";
    public const string IDP_SSO_ENDPOINT = "/idp/startSSO.ping";
    public const string SP_SLO_ENDPOINT = "/sp/startSLO.ping";
    public const string AGENTLESS_BASE = "AgentlessSPSample/app";

    // POST keys
    public const string REF = "REF";
    public const string RESUME_PATH = "resumePath";
    public const string CURRENT_BASE_URL = "currentBaseUrl";

    // adapter configuration keys
    public const string ADAPTER_CONFIG_SECTION = "SPSampleAppConfiguration";
    public const string BASE_PF_URL = "basePfUrl";
    public const string ADAPTER_USERNAME = "username";
    public const string ADAPTER_PASSWORD = "passphrase";
    public const string ADAPTER_ID = "adapterId";
    public const string TARGET_URL = "targetURL";
    public const string PARTNER_ENTITY_ID = "partnerEntityId";

    // PF headers
    public const string PING_ADAPTER_HEADER = "ping.instanceId";

}