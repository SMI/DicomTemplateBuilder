namespace Repopulator.Matchers
{
    /// <summary>
    /// Interface for classes that match Csv rows to files on disk.  This could be as simple as following a file URI or as
    /// complicated as reading a UID returning the corresponding files
    /// </summary>
    public interface IRepopulatorMatcher
    {
        /// <summary>
        /// Returns the next file to be repopulated and the corresponding values to overwrite with.  Returns
        /// null if there are no more files/rows to process
        /// </summary>
        RepopulatorJob Next();
    }
}