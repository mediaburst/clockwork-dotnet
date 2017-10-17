namespace Clockwork
{
	/// <summary>
	/// Action to take if the message text contains an invalid character
	/// Valid characters are defined in the GSM 03.38 character set
	/// </summary>
	public enum InvalidCharacterAction
	{
		/// <summary>
		/// Use the default setting from your account
		/// </summary>
		AccountDefault = 0,
		/// <summary>
		/// Return an error if a Non-GSM character is found
		/// </summary>
		Error = 1,
		/// <summary>
		/// Remove any Non-GSM character
		/// </summary>
		Remove = 2,
		/// <summary>
		/// Replace Non-GSM characters where possible
		/// remove any others
		/// </summary>
		Replace = 3
	}
}