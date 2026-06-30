namespace book_bracket.Services.Interfaces
{
    /// <summary>
    /// Interacts between user and application.
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Presents user with <paramref name="choices"/> and allows them to pick one.
        /// </summary>
        /// <param name="title">Introductory message.</param>
        /// <param name="choices">Choices to display to user.</param>
        /// <returns>User choice.</returns>
        string Choose(string title, params string[] choices);
        /// <summary>
        /// Reads user input.
        /// </summary>
        /// <param name="message">Optional message to display to user.</param>
        /// <returns>User input.</returns>
        string? Read(string? message = null);
        /// <summary>
        /// Displays warning to user.
        /// </summary>
        /// <param name="message">Warning to display to user.</param>
        void Warn(string message);
        /// <summary>
        /// Displays <paramref name="message"/> to user.
        /// </summary>
        /// <param name="message">Message to display to user.</param>
        void Write(string message);
    }
}