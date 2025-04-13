namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsPerson" />
    /// </summary>
    public class clsPerson
    {
        /// <summary>
        /// Defines the enMode
        /// </summary>
        public enum enMode
        {
            /// <summary>
            /// Defines the AddNew
            /// </summary>
            AddNew,
            /// <summary>
            /// Defines the Update
            /// </summary>
            Update
        }

        /// <summary>
        /// Gets or sets the Mode
        /// </summary>
        public enMode Mode { get; set; } = enMode.AddNew;

        /// <summary>
        /// Gets or sets the PersonID
        /// </summary>
        public int PersonID { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the SecondName
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or sets the ThirdName
        /// </summary>
        public string ThirdName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the UniversityID
        /// </summary>
        public int UniversityID { get; set; }

        /// <summary>
        /// Gets or sets the ContactEmail
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the IsEmployee
        /// </summary>
        public bool? IsEmployee { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedAt
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the Gendor
        /// </summary>
        public int? Gendor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsPerson"/> class.
        /// </summary>
        public clsPerson()
        {
            this.PersonID = default;
            this.FirstName = default;
            this.SecondName = default;
            this.ThirdName = default;
            this.LastName = default;
            this.UniversityID = default;
            this.ContactEmail = default;
            this.IsEmployee = default;
            this.CreatedAt = default;
            this.UpdatedAt = default;
            this.Gendor = default;
            Mode = enMode.AddNew;
        }

        public PersonDTO personDTO
        {
            get
            {
                return new(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                            this.UniversityID, this.ContactEmail, this.IsEmployee, this.CreatedAt,
                            this.UpdatedAt, this.Gendor);
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsPerson"/> class from being created.
        /// </summary>
        /// <param name="personDTO">The personDTO<see cref="PersonDTO"/></param>
        private clsPerson(PersonDTO personDTO , enMode mode = enMode.AddNew)
        {
            this.PersonID = personDTO.PersonID;
            this.FirstName = personDTO.FirstName;
            this.SecondName = personDTO.SecondName;
            this.ThirdName = personDTO.ThirdName;
            this.LastName = personDTO.LastName;
            this.UniversityID = personDTO.UniversityID;
            this.ContactEmail = personDTO.ContactEmail;
            this.IsEmployee = personDTO.IsEmployee;
            this.CreatedAt = personDTO.CreatedAt;
            this.UpdatedAt = personDTO.UpdatedAt;
            this.Gendor = personDTO.Gendor;
            Mode = mode;
        }

        /// <summary>
        /// The _AddNewPersonAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewPersonAsync()
        {
            this.PersonID = await clsPersonData.AddNewPersonAsync(personDTO);
            return this.PersonID != 0;
        }

        /// <summary>
        /// The _UpdatePersonAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdatePersonAsync()
        {
            return await clsPersonData.UpdatePersonAsync(personDTO);
        }

        /// <summary>
        /// The DeletePersonAsync
        /// </summary>
        /// <param name="PersonID">The PersonID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeletePersonAsync(int PersonID)
        {
            return await clsPersonData.DeletePersonAsync(PersonID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="PersonID">The PersonID<see cref="int"/></param>
        /// <returns>The <see cref="clsPerson"/></returns>
        public static clsPerson Find(int PersonID)
        {
            PersonDTO personDTO = clsPersonData.GetPersonByPersonID(PersonID);
            if (personDTO != null)
            {
                return new clsPerson(personDTO, enMode.Update);
            }
            else
                return null;
        }

        /// <summary>
        /// The IsPersonExistsAsync
        /// </summary>
        /// <param name="PersonID">The PersonID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsPersonExistsAsync(int PersonID)
        {
            return await clsPersonData.IsPersonExistsAsync(PersonID);
        }

        /// <summary>
        /// The GetAllPersonAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{PersonDTO}}"/></returns>
        public static async Task<List<PersonDTO>> GetAllPersonAsync()
        {
            return await clsPersonData.GetAllPersonAsync();
        }

        /// <summary>
        /// The Save
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (await _AddNewPersonAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdatePersonAsync();
            }

            return false;
        }
    }
}