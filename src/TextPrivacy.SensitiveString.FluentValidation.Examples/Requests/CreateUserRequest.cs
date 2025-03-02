namespace TextPrivacy.SensitiveString.FluentValidation.Examples.Requests;

public class CreateUserRequest
{
    public SensitiveString? FirstName { get; init; }
    public SensitiveString? LastName { get; init; }
    public SensitiveEmail? PrimaryEmail { get; init; }
    public List<SensitiveEmail>? SecondaryEmails { get; init; }
    public List<SensitiveString>? AddressLines { get; init; }
}