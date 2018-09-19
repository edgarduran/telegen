using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    /// <summary>
    /// Performs file-related operations.
    /// </summary>
    public interface IFileAgent
    {

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="msg">The Operation object describing the file to create.</param>
        /// <returns>The results of the operation, or null if no action was taken.</returns>
        FileActivityResult CreateFile(OpCreateFile msg);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="msg">The Operation object describing the file to delete.</param>
        /// <returns>The results of the operation, or null if no action was taken.</returns>
        FileActivityResult DeleteFile(OpDeleteFile msg);

        /// <summary>
        /// Updates a file.
        /// </summary>
        /// <param name="msg">The Operation object describing the file to update.
        /// Currently, only appends are supported. Could be modified to support more
        /// elaborate operations.
        /// </param>
        /// <returns>The results of the operation, or null if no action was taken.</returns>
        FileActivityResult UpdateFile(OpUpdateFile msg);
    }
}