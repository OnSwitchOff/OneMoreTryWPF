using OneMoreTryWPF.SessionService;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneMoreTryWPF.ENUMs;
using System.Windows;

namespace OneMoreTryWPF.Facades
{
	static class SessionServiceOperationsFacade
	{
		static private SessionServiceClient serviceClient;

		static private SessionServiceClient getServiceClient()
		{
			if (serviceClient == null || ConfigManagerFacade.isSessionServiceConfigChanged())
			{
				#region custom binding

				CustomBinding customBinding = new CustomBinding();

				TransportSecurityBindingElement security = TransportSecurityBindingElement.CreateUserNameOverTransportBindingElement();
				security.IncludeTimestamp = false;
				security.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic256;
				security.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;

				TextMessageEncodingBindingElement encoding = new TextMessageEncodingBindingElement();
				encoding.MessageVersion = MessageVersion.Soap11;

				HttpsTransportBindingElement transport = new HttpsTransportBindingElement();
				transport.MaxReceivedMessageSize = ConfigManagerFacade.getSessionService_MaxReceivedMessageSize();

				customBinding.Elements.Add(security);
				customBinding.Elements.Add(encoding);
				customBinding.Elements.Add(transport);
				#endregion

				string EndpointAddressString = ConfigManagerFacade.getSessionService_EndpointAddress();
				EndpointAddress endpointAdress = new EndpointAddress(EndpointAddressString);

				SessionServiceClient sessionService = new SessionServiceClient(customBinding, endpointAdress);
				sessionService.ClientCredentials.UserName.UserName = SessionDataManagerFacade.getUserName();
				sessionService.ClientCredentials.UserName.Password = SessionDataManagerFacade.getPassword();
				serviceClient = sessionService;
			}
			return serviceClient;
		}


		static public bool StartSession()
		{
			switch (CurrentSessionStatus())
			{
				case SessionStatusType.NO_CONNECTION:
					return false;
				case SessionStatusType.OK:
					return true;
				case SessionStatusType.NOT_FOUND:
					CloseSessionByCredentials();
					return CreateSession();
				case SessionStatusType.CLOSED:
					return CreateSession();
				default:
					return false;
			}
		}

		static public bool CreateSession()
		{
			createSessionRequest CreateSessionRequest = new createSessionRequest();
			CreateSessionRequest.tin = getServiceClient().ClientCredentials.UserName.UserName;
			CreateSessionRequest.x509Certificate = SessionDataManagerFacade.getX509AuthCertificate();

			createSessionResponse CreateSessionResponse;
			try
			{
				CreateSessionResponse = getServiceClient().createSession(CreateSessionRequest);
				SessionDataManagerFacade.setSessionId(CreateSessionResponse.sessionId);
				return getCurrentUser() && getCurrentUserProfile();
			}
			catch (Exception)
			{
				CloseSessionByCredentials();
				return false;
			}
		}

		static public bool CloseSessionByCredentials()
		{
			closeSessionByCredentialsRequest CloseSessionByCredentialsRequest = new closeSessionByCredentialsRequest();
			CloseSessionByCredentialsRequest.tin = getServiceClient().ClientCredentials.UserName.UserName;
			CloseSessionByCredentialsRequest.x509Certificate = SessionDataManagerFacade.getX509AuthCertificate();

			closeSessionResponse CloseSessionByCredentialsResponse;
			try
			{
				CloseSessionByCredentialsResponse = getServiceClient().closeSessionByCredentials(CloseSessionByCredentialsRequest);
				SessionDataManagerFacade.clearSessionData();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool CloseSession()
		{
			switch (CurrentSessionStatus())
			{
				case SessionStatusType.NO_CONNECTION:
					return false;
				case SessionStatusType.OK:
					closeSessionRequest CloseSessionRequest = new closeSessionRequest();
					CloseSessionRequest.sessionId = SessionDataManagerFacade.getSessionId();
					closeSessionResponse CloseSessionResponse;
					try
					{
						CloseSessionResponse = getServiceClient().closeSession(CloseSessionRequest);
						SessionDataManagerFacade.clearSessionData();
						return true;
					}
					catch (Exception)
					{
						return false;
					}
				case SessionStatusType.NOT_FOUND:
				case SessionStatusType.CLOSED:
					SessionDataManagerFacade.clearSessionData();
					return true;
				default:
					return false;
			}
		}

		internal static SessionStatusType CurrentSessionStatus()
		{
			if (!SessionDataManagerFacade.isEmptySessionId())
			{
				currentSessionStatusRequest CurrentSessionStatusRequest = new currentSessionStatusRequest();
				CurrentSessionStatusRequest.sessionId = SessionDataManagerFacade.getSessionId();

				currentSessionStatusResponse CurrentSessionStatusResponse;
				try
				{
					CurrentSessionStatusResponse = getServiceClient().currentSessionStatus(CurrentSessionStatusRequest);
					switch (CurrentSessionStatusResponse.status)
					{
						case sessionStatus.OK:
							return SessionStatusType.OK;
						case sessionStatus.CLOSED:
							return SessionStatusType.CLOSED;
						case sessionStatus.NOT_FOUND:
							return SessionStatusType.NOT_FOUND;
					}
				}
				catch (Exception)
				{
					return SessionStatusType.NO_CONNECTION;
				}
			}
			return SessionStatusType.NOT_FOUND;
		}

		internal static bool getCurrentUser()
		{
			switch (CurrentSessionStatus())
			{
				case SessionStatusType.OK:
					currentUserRequest CurrentUserRequest = new currentUserRequest();
					CurrentUserRequest.sessionId = SessionDataManagerFacade.getSessionId();
					currentUserResponse CurrentUserResponse;
					try
					{
						CurrentUserResponse = getServiceClient().currentUser(CurrentUserRequest);
						SessionDataManagerFacade.setCurrentUserData(CurrentUserResponse.user);
						return true;
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message);
						return false;
					}

				case SessionStatusType.NO_CONNECTION:
				case SessionStatusType.NOT_FOUND:
				case SessionStatusType.CLOSED:
				default:
					return false;
			}
		}

		internal static bool getCurrentUserProfile()
		{
			switch (CurrentSessionStatus())
			{
				case SessionStatusType.OK:
					currentUserProfilesRequest CurrentUserProfilesRequest = new currentUserProfilesRequest();
					CurrentUserProfilesRequest.sessionId = SessionDataManagerFacade.getSessionId(); ;
					currentUserProfilesResponse CurrentUserProfilesResponse;
					try
					{
						CurrentUserProfilesResponse = getServiceClient().currentUserProfiles(CurrentUserProfilesRequest);
						SessionDataManagerFacade.setCurrentUserProfilesData(CurrentUserProfilesResponse.profileInfoList);
						return true;
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message);
						return false;
					}

				case SessionStatusType.NO_CONNECTION:
				case SessionStatusType.NOT_FOUND:
				case SessionStatusType.CLOSED:
				default:
					return false;
			}
		}
	}
}
