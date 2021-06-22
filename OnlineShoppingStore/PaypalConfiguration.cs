using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace OnlineShoppingStore
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PaypalConfiguration()
        {
            var config = GetConfig();
            clientId =config  ["AWFq7LsNq751085crCih96LaEjkenzc_PQJCrUIURtdaPCvcOvm_n8dG5KiQ1RF5E7SqAu9O3ZNqTr7h"];
            clientSecret =config ["EEgPT1Au08_SC_oYE5bZ9ma-lZnnDP9Qukf_78ijSStuow07uE03_icg42mDmLBLLwlyP8ayWoTxWGEP"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, GetConfig()).GetAccessToken();
            //string accessToken = GetPayPalAccessToken(clientId, clientSecret);
            return accessToken;
        }

        private static string GetPayPalAccessToken(string clientId, string clientSecret)
        {
            string PaypalClientID = clientId;
            string PaypalSecret = clientSecret;

            string AccessToken = "";

            try
            {
                OAuthTokenCredential tokenCredential = new OAuthTokenCredential(PaypalClientID, PaypalSecret);
                AccessToken = tokenCredential.GetAccessToken();
            }
            catch (PayPal.IdentityException ex)
            {
                // ex.Details provides quick access to the API error information
            }
            catch (PayPal.HttpException ex)
            {
                //Some other error occurred when attempting to send the request to PayPal
                //ex.Response contains the full response from the API which should highlight the error encountered.
            }
            catch (PayPal.PayPalException ex)
            {
                //A General exception was thrown from the SDK
            }
            catch (System.Exception ex)
            {
                //Some other general exception occurred.
            }

            return AccessToken;
        }

               
         public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = GetConfig();
            return apicontext;
        }


    }
}