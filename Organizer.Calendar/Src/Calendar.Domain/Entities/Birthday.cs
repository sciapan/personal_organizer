namespace Calendar.Domain.Entities
{
    /// <summary>
    /// Simple entity to store information about person birthday.
    /// </summary>
    public class Birthday
    {
        #region Properties

        /// <summary>
        /// Unique id of record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public required DateTimeOffset Dob { get; set; }

        /// <summary>
        /// Person name.
        /// </summary>
        public required string Person { get; set; }

        /// <summary>
        /// Additional notes.
        /// </summary>
        public string? Notes { get; set; }

        #endregion
    }
}