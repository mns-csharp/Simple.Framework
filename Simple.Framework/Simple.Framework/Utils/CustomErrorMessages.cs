using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    internal class CustomErrorMessage
    {
        public const string IDsMustNotBeSetManually = "IDs must not be set manually!";
        public const string ConnectionStringPropertyHasNotBeenInitialized = "The ConnectionString property has not been initialized.";
        public const string TransactionIsNotCommittable = "Transaction is not commitable!";
        public const string WhereIsProviderName = "Where is providerName?";
        public const string SqlConnectionDoesNotSupportParallelTransactions = "SqlConnection does not support parallel transactions.";
        public const string NoTransactionToBeCommitted = "No transaction to be committed!";
        public const string ParametersAreNotConsistentWithColumnNames = "Parameters are not consistent with column names.";
        public const string ObjectReferenceNotSetToAnInstanceOfAnObject = "Object reference not set to an instance of an object.";
        public const string IdIncrementTypeIsNotSet = "Id increment type is not set.";
        public const string DataObjectIsNotSet = "Data object is not set.";
        public const string NewObjectCanNotBeDeleted = "New object can not be deleted.";
        public const string NoRowWasSaved = "No object was Saved!";
        public const string NoRowWasSavedOrUpdated = "No object was SaveOrUpdated!";
        public const string NoRowWasUpdated = "No object was Updated!";
        public const string NoRowWasDeleted = "No object was Deleted!";
        public const string DataTypeMustBeNullable = "The datatype must be Nullable<T>";
    }
}
