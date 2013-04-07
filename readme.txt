Modeled to facilitate WCF hosted services support async-operations easier - allowing focus on application services.
Use at your own risk; YMMV; not to be used with MAO inhibitors...

In your service contract interface define only asynch service methods (and decorate appropriately):
...
	/// <summary>
        /// Begins to remove assignments for current user.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        [OperationContract(AsyncPattern = true)]
        [FaultContract(typeof(Exception))]
        [WebInvoke(UriTemplate = "/RemoveAssignments?criteria={criteria}",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        IAsyncResult BeginRemoveAssignments(IEnumerable<Report> criteria, AsyncCallback callback, object state);

        /// <summary>
        /// Ends the remove assignments for current user.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns>
        /// The enumerable collection of reports.
        /// </returns>
        IEnumerable<SubmissionAccepted> EndRemoveAssignments(IAsyncResult asyncResult);
...

In your service-layer - leverage the Task Helper to defer processing:
...
	/// <summary>
        /// Begins to remove assignments for current user.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public IAsyncResult BeginRemoveAssignments(IEnumerable<Report> criteria, AsyncCallback callback, object state)
        {
            string userContext = System.Web.HttpContext.Current.User.Identity.Name;
            return Task<IEnumerable<SubmissionAccepted>>.Factory.StartNew(
                () =>
                    {
                        var q = new ConcurrentBag<SubmissionAccepted>();
                        criteria.AsParallel().ForAll(
                            reportCriteria => q.Add(RemoveAssignment(reportCriteria, userContext)));
                        return q;

                    }).CreateAsyncResult(callback, state);
        }

        /// <summary>
        /// Ends the remove assignments for current user.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns>
        /// The enumerable collection of reports.
        /// </returns>
        public IEnumerable<SubmissionAccepted> EndRemoveAssignments(IAsyncResult asyncResult)
        {
            return TaskHelper.InterpretResult<IEnumerable<SubmissionAccepted>>(asyncResult);
        }

...