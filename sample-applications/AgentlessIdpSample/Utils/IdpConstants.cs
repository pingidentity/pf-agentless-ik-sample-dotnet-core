static class IdpConstants
{
    // endpoints
    public const string PICKUP_ENDPOINT = "/ext/ref/pickup";
    public const string DROPOFF_ENDPOINT = "/ext/ref/dropoff";
    public const string AGENTLESS_BASE = "AgentlessIdPSample/app";
    public const string START_SP_SSO = "/sp/startSSO.ping";

    // attribute keys
    public const string SUBJECT = "subject";
    public const string AUTH_INST = "authnInst";

    // POST keys
    public const string USERNAME = "username";
    public const string PASSWORD = "password";
    public const string REF = "REF";
    public const string RESUME_PATH = "resumePath";
    public const string CURRENT_BASE_URL = "currentBaseUrl";

    // adapter configuration keys
    public const string ADAPTER_CONFIG_SECTION = "IdPSampleAppConfiguration";
    public const string BASE_PF_URL = "basePfUrl";
    public const string ADAPTER_USERNAME = "username";
    public const string ADAPTER_PASSWORD = "passphrase";
    public const string ADAPTER_ID = "adapterId";
    public const string TARGET_URL = "targetURL";
    public const string PARTNER_ENTITY_ID = "partnerEntityId";

    // PF headers
    public const string PING_ADAPTER_HEADER = "ping.instanceId";
}