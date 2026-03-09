using FluentAssertions;
using Max.Bot.Types;
using Xunit;

namespace Max.Bot.Tests.Unit.Types;

public class ContactHelpersTests
{
    [Fact]
    public void ParseVcf_WithValidVcf_ShouldExtractPhoneNumber()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            FN:John Doe
            TEL:+1234567890
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.PhoneNumber.Should().Be("+1234567890");
        contactInfo.FullName.Should().Be("John Doe");
    }

    [Fact]
    public void ParseVcf_WithTelParameter_ShouldExtractPhoneNumber()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            FN:Jane Smith
            TEL;TYPE=CELL:+9876543210
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.PhoneNumber.Should().Be("+9876543210");
        contactInfo.FullName.Should().Be("Jane Smith");
    }

    [Fact]
    public void ParseVcf_WithNField_ShouldExtractFirstNameAndLastName()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            N:Doe;John;;;
            FN:John Doe
            TEL:+1234567890
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FirstName.Should().Be("John");
        contactInfo.LastName.Should().Be("Doe");
        contactInfo.FullName.Should().Be("John Doe");
    }

    [Fact]
    public void ParseVcf_WithoutFn_ShouldConstructFullNameFromNField()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            N:Smith;Jane;;;
            TEL:+1111111111
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FirstName.Should().Be("Jane");
        contactInfo.LastName.Should().Be("Smith");
        contactInfo.FullName.Should().Be("Jane Smith");
    }

    [Fact]
    public void ParseVcf_WithNullInput_ShouldReturnNull()
    {
        // Act
        var contactInfo = ContactHelpers.ParseVcf(null);

        // Assert
        contactInfo.Should().BeNull();
    }

    [Fact]
    public void ParseVcf_WithEmptyInput_ShouldReturnNull()
    {
        // Act
        var contactInfo = ContactHelpers.ParseVcf(string.Empty);

        // Assert
        contactInfo.Should().BeNull();
    }

    [Fact]
    public void ParseVcf_WithWhitespaceInput_ShouldReturnNull()
    {
        // Act
        var contactInfo = ContactHelpers.ParseVcf("   ");

        // Assert
        contactInfo.Should().BeNull();
    }

    [Fact]
    public void ParseVcf_WithFnParameters_ShouldExtractFullName()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            FN;CHARSET=UTF-8:John Doe
            TEL:+1234567890
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FullName.Should().Be("John Doe");
    }

    [Fact]
    public void ParseVcf_WithFoldedLines_ShouldExtractFullName()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            FN:John
             Doe
            TEL:+1234567890
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FullName.Should().Be("JohnDoe");
    }

    [Fact]
    public void ParseVcf_WithEscapedCharacters_ShouldUnescape()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            FN:John\; Jr. Doe
            TEL:+1234567890
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FullName.Should().Be("John; Jr. Doe");
    }

    [Fact]
    public void ParseVcf_WithNFieldOnlyLastname_ShouldSetFullName()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            N:Smith;;;;
            TEL:+1111111111
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.LastName.Should().Be("Smith");
        contactInfo.FullName.Should().Be("Smith");
    }

    [Fact]
    public void ParseVcf_WithNFieldOnlyFirstname_ShouldSetFullName()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            N:;John;;;
            TEL:+1111111111
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FirstName.Should().Be("John");
        contactInfo.FullName.Should().Be("John");
    }

    [Fact]
    public void ParseVcf_WithEmptyNField_ShouldNotCrash()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            N:
            TEL:+1111111111
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FirstName.Should().BeNull();
        contactInfo.LastName.Should().BeNull();
    }

    [Fact]
    public void ParseVcf_WithMultipleTelFields_ShouldUseFirst()
    {
        // Arrange
        var vcf = """
            BEGIN:VCARD
            VERSION:3.0
            TEL:+1111111111
            TEL;TYPE=CELL:+2222222222
            TEL;TYPE=WORK:+3333333333
            END:VCARD
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.PhoneNumber.Should().Be("+1111111111");
    }

    [Fact]
    public void ParseVcf_WithoutBeginEnd_ShouldStillParse()
    {
        // Arrange
        var vcf = """
            FN:John Doe
            TEL:+1234567890
            """;

        // Act
        var contactInfo = ContactHelpers.ParseVcf(vcf);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FullName.Should().Be("John Doe");
        contactInfo.PhoneNumber.Should().Be("+1234567890");
    }

    [Fact]
    public void GetPhoneNumber_FromMaxInfo_ShouldReturnPhoneNumber()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nTEL:+9999999999\nEND:VCARD",
            MaxInfo = new ContactInfo
            {
                PhoneNumber = "+1234567890"
            }
        };

        // Act
        var phoneNumber = ContactHelpers.GetPhoneNumber(contact);

        // Assert
        phoneNumber.Should().Be("+1234567890");
    }

    [Fact]
    public void GetPhoneNumber_FromVcfInfo_WhenMaxInfoIsNull_ShouldParsePhoneNumber()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nTEL:+9876543210\nEND:VCARD",
            MaxInfo = null
        };

        // Act
        var phoneNumber = ContactHelpers.GetPhoneNumber(contact);

        // Assert
        phoneNumber.Should().Be("+9876543210");
    }

    [Fact]
    public void GetPhoneNumber_FromVcfInfo_WhenMaxInfoPhoneNumberIsNull_ShouldParsePhoneNumber()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nTEL:+5555555555\nEND:VCARD",
            MaxInfo = new ContactInfo
            {
                PhoneNumber = null
            }
        };

        // Act
        var phoneNumber = ContactHelpers.GetPhoneNumber(contact);

        // Assert
        phoneNumber.Should().Be("+5555555555");
    }

    [Fact]
    public void GetPhoneNumber_WithNullContact_ShouldReturnNull()
    {
        // Act
        var phoneNumber = ContactHelpers.GetPhoneNumber(null);

        // Assert
        phoneNumber.Should().BeNull();
    }

    [Fact]
    public void GetPhoneNumber_WithNoPhoneNumber_ShouldReturnNull()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nFN:No Phone\nEND:VCARD",
            MaxInfo = new ContactInfo()
        };

        // Act
        var phoneNumber = ContactHelpers.GetPhoneNumber(contact);

        // Assert
        phoneNumber.Should().BeNull();
    }

    [Fact]
    public void GetFullName_FromMaxInfo_ShouldReturnFullName()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nFN:Other Name\nEND:VCARD",
            MaxInfo = new ContactInfo
            {
                FullName = "John Doe"
            }
        };

        // Act
        var fullName = ContactHelpers.GetFullName(contact);

        // Assert
        fullName.Should().Be("John Doe");
    }

    [Fact]
    public void GetFullName_FromVcfInfo_WhenMaxInfoIsNull_ShouldParseFullName()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nFN:Jane Smith\nEND:VCARD",
            MaxInfo = null
        };

        // Act
        var fullName = ContactHelpers.GetFullName(contact);

        // Assert
        fullName.Should().Be("Jane Smith");
    }

    [Fact]
    public void GetFullName_FromVcfInfo_WhenMaxInfoFullNameIsNull_ShouldParseFullName()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nFN:Bob Johnson\nEND:VCARD",
            MaxInfo = new ContactInfo
            {
                FullName = null
            }
        };

        // Act
        var fullName = ContactHelpers.GetFullName(contact);

        // Assert
        fullName.Should().Be("Bob Johnson");
    }

    [Fact]
    public void GetFullName_WithNullContact_ShouldReturnNull()
    {
        // Act
        var fullName = ContactHelpers.GetFullName(null);

        // Assert
        fullName.Should().BeNull();
    }

    [Fact]
    public void GetPhoneNumber_WithEmptyVcf_ShouldReturnNull()
    {
        // Arrange
        var contact = new Contact
        {
            VcfInfo = "BEGIN:VCARD\nVERSION:3.0\nEND:VCARD",
            MaxInfo = null
        };

        // Act
        var phoneNumber = ContactHelpers.GetPhoneNumber(contact);

        // Assert
        phoneNumber.Should().BeNull();
    }

    [Fact]
    public void ContactInfo_NameProperty_ShouldMapToFullName()
    {
        // Arrange
        var contactInfo = new ContactInfo();

        // Act
        contactInfo.Name = "Test User";

        // Assert
        contactInfo.FullName.Should().Be("Test User");
        contactInfo.Name.Should().Be("Test User");
    }

    [Fact]
    public void ContactInfo_NameProperty_WithJsonDeserialization_ShouldWork()
    {
        // Arrange
        var json = """{"user_id":123,"name":"John Doe","username":"johndoe"}""";

        // Act
        var contactInfo = Max.Bot.Networking.MaxJsonSerializer.Deserialize<ContactInfo>(json);

        // Assert
        contactInfo.Should().NotBeNull();
        contactInfo!.FullName.Should().Be("John Doe");
        contactInfo.Name.Should().Be("John Doe");
    }
}
