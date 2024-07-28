namespace BMS.DTO.Common
{
    public class CurrentSession
    {
        public int ClientId { get; set; }
        public int? ClientRoleId { get; set; }
        public string IsRequiredUserCredential { get; set; }
        public string IsAuthorizationRequired { get; set; }
        public int TenantId { get; set; }
        public int? UserId { get; set; }
        public int? UserRoleId { get; set; }
        public string UserFullName { get; set; }
        public string DisplayId { get; set; }
        public int TimeZoneOffset { get; set; }
    }
}
