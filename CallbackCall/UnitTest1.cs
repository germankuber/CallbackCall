using System;
using Xunit;

namespace CallbackCall
{
    public class UnitTest1
    {
        [Fact]
        public void Invoke_Method()
        {
            var _sut = new HttpManager();

            _sut.CallService(CallServicesRequest.Make(TimeOutError, ServerError, SuccessValue)
                            .Build("http", "Domain"));
        }
        private void SuccessValue(int obj)
        {
            throw new NotImplementedException();
        }

        private void ServerError(string obj)
        {
            throw new NotImplementedException();
        }

        private void TimeOutError()
        {
            throw new NotImplementedException();
        }

    }

    public class HttpManager
    {
        public void CallService(CallServicesRequest request)
        {

        }
    }

    public interface IRequestCheckCall
    {
        public CallServicesRequest Build(string httpValue, string domain);
    }
    public class CallServicesRequest : IRequestCheckCall
    {
        private readonly Action _timeOutError;
        private readonly Action<string> _serverError;
        private readonly Action<int> _successValue;

        private CallServicesRequest(Action timeOutError, Action<string> serverError, Action<int> successValue)
        {
            _timeOutError = timeOutError;
            _serverError = serverError;
            _successValue = successValue;
        }

        public static IRequestCheckCall Make(Action timeOutError, Action<string> serverError, Action<int> successValue)
        {
            return new CallServicesRequest(timeOutError, serverError, successValue);
        }

        public string HttpValue { get; set; }
        public string Domain { get; set; }
        public CallServicesRequest Build(string httpValue, string domain)
        {
            HttpValue = httpValue;
            Domain = domain;
            return this;
        }
        public void TimeOut() => _timeOutError();
        public void Error(string error) => _serverError(error);
        public void Success(int success) => _successValue(success);
    }


}
