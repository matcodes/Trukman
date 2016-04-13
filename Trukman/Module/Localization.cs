using System;

namespace Trukman
{
	public class Localization
	{	
		public static Languages language;

		public enum Languages{
			ENGLISH,
			ESPANIOL
		};

		public enum LocalStrings{
			WELCOME,
			COMPANY_NAME,
			COMPANY_ADRESS,
			PROCEED_TO_FLEET_SIZE,
			SEND,
			FULL_NAME,
			FIRST_NAME,
			LAST_NAME,
			PHONE,
			COMPANY_YOU_WORK_FOR,
			MC,
			ENTER,
			BY_CLICKING_ENTER_YOU_AGREE_TO_THE,
			TERMS_AND_CONDITIONS,
			SIGN_UP_AS,
			OWNER_or_OPERATOR_OR_FLEET,
			DISPATCH,
			DRIVER,
			NEXT,
			MANAGE_DRIVERS,
			MANAGE_DISPATCH,
			MANAGE_JOBS,
			MANAGE_FLEET,
			SETTING,
			HELP,
			JOB_NAME,
			JOB_DESCRIPTION,
			SHIPPER_ADDRESS,
			RECEIVE_ADDRESS,
			BTN_SAVE,
			BTN_CANCEL,
			CAMBIAR_A_ESPANOL,
			OWNER,
			SUBMIT,
			SIGN_UP,
			HAVE_ACCOUNT_QUESTION,
			CANCEL_AUTHORIZATION_REQUEST,
			WAITING_FOR_COMPANY_TO_AUTHORIZE,
			AUTHORIZED_BY_COMPANY,
			CONTINUE,
            DBA,
            PHYS_ADDRESS,
            FLEET_SIZE,
            EMAIL,
            NAME,
            FAILED_OWNER_MC,
            OWNER_or_OPERATOR
		};

		static string[] engStrings = {
			"Welcome to Trukman. Let's get you set-­up" ,
			"COMPANY NAME",
			"COMPANY ADDRESS",
			"PROCEED TO FLEET SIZE",
			"SEND",
			"FULL NAME",
			"First Name",
			"Last Name",
			"Phone",
			"Company Name",
			"MC #",
			"ENTER",
			"By clicking Enter you agree to the",
			"Terms and Conditions",
			"SIGN IN AS", // "Sign Up As",
			"OWNER/OPERATOR OR FLEET",
			"Dispatch",
			"Driver",
			"Next",
			"Manage Drivers",
			"Manage Dispatch",
			"Manage Jobs", 
			"Manage Fleet",
			"Setting",
			"Help",
			"Job name",
			"Job description",
			"Shipper address",
			"Receive address",
			"Save",
			"Cancel",
			"Cambiar a Espanol",
			"O/O",
			"Submit",
			"Sign up",
			"Already have an account?",
			"Cancel Authorization Request",
			"Waiting for {0} to authorize you",
			"Tou have been authorized by {0}",
			"Continue",
            "DBA",
            "Physical Address",
            "Fleet size",
            "Email",
            "Name",
            "Sorry you are having trouble signing up. Please email help@trukman.com for assistance.",
            "Owner/Operator"
		};

		static string[] espStrings = {
			"Bienvenidos a Trukman! empecemos con el registro",
			"NOMBRE DE EMRESA",
			"DIRECION DE EMPRESA",
			"CONTIDAD DE CAMIONES",
			"MANDAR",
			"NOMBRE COMPLETO",
			"First Name",
			"Last Name",
			"Telefono",
			"EMPRESA PARA LA QUE TRABAJA",
			"Numero de MC ",
			"ENTAR",
			"Haciendo clic introduce que usted está de acuerdo con los",
			"términos y condiciones",
			"Registrece como",
			"DUENO OPERADOR",
			"Despachador",
			"Conductor",
			"Próximo",
			//not tranclate
			"Manage Drivers",
			"Manage Dispatch",
			"Manage Jobs", 
			"Manage Fleet",
			"Setting",
			"Help",
			"Job name",
			"Job description",
			"Shipper address",
			"Receive address",
			"Save",
			"Cancel",
			"Cambiar a Espanol",
			"O/O",
			"Submit",
			"Sign up",
			"Already have an account?",
			"Cancel Authorization Request",
			"Waiting for {0} to authorize you",
			"Tou have been authorized by {0}",
			"Continue",
            "DBA",
            "Physical Address",
            "Fleet size",
            "Email",
            "Name",
            "Sorry you are having trouble signing up. Please email help@trukman.com for assistance.",
            "Owner/Operator"
		};



		public static string getString(LocalStrings name) {
			if (language == Languages.ENGLISH) {
				return engStrings [(int)name];
			} else {
				return espStrings [(int)name];
			}
		}
	}
}

