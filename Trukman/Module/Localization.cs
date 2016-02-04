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
			MANAGE_FLEET,
			SETTING,
			HELP,
			CAMBIAR_A_ESPANOL
		};

		static string[] engStrings = {
			"Welcome to Trukman. Let's get you set-­up" ,
			"COMPANY NAME",
			"COMPANY ADDRESS",
			"PROCEED TO FLEET SIZE",
			"SEND",
			"FULL NAME",
			"PHONE",
			"COMPANY YOU WORK FOR",
			"MC #",
			"ENTER",
			"By clicking Enter you agree to the",
			"Terms and Conditions",
			"Sign Up As",
			"OWNER/OPERATOR OR FLEET",
			"DISPATCH",
			"DRIVER",
			"Next",
			"Manage Drivers",
			"Manage Dispatch",
			"Manage Fleet",
			"Setting",
			"Help",
			"Cambiar a Espanol"
		};

		static string[] espStrings = {
			"Bienvenidos a Trukman! empecemos con el registro",
			"NOMBRE DE EMRESA",
			"DIRECION DE EMPRESA",
			"CONTIDAD DE CAMIONES",
			"MANDAR",
			"NOMBRE COMPLETO",
			"TELEFONO",
			"EMPRESA PARA LA QUE TRABAJA",
			"Numero de MC ",
			"ENTAR",
			"Haciendo clic introduce que usted está de acuerdo con los",
			"términos y condiciones",
			"Registrece como",
			"DUENO OPERADOR",
			"DESPACHADOR",
			"CONDUCTOR",
			"Próximo",
			//not tranclate
			"Manage Drivers",
			"Manage Dispatch",
			"Manage Fleet",
			"Setting",
			"Help",
			"Cambiar a Espanol"
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

