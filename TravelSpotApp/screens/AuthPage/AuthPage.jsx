import { useState } from 'react';
import { Text } from 'react-native';

import Login from '../../components/Login/Login';
import Registration from '../../components/Login/Registration';
import { StyledAuthPageWrapper } from '../../components/Login/styledAuth';
import PasswordRecovery from '../../components/Login/PasswordRecovery';

const AuthPage = ({basicAction, setCurrentPage}) => {

    const [authAction, setAuthAction] = useState(basicAction !== '' ? basicAction : 'login')

    return(
        <StyledAuthPageWrapper>
            {
                authAction === 'login' ? <Login setAuthAction={setAuthAction} setCurrentPage={setCurrentPage}/> 
                : authAction === 'registration' ? <Registration setAuthAction={setAuthAction} setCurrentPage={setCurrentPage}/>
                : authAction === 'psswdConfirmation' ? <Text>Confirm password</Text>
                : authAction === 'psswdRecovery' ? <PasswordRecovery setAuthAction={setAuthAction}/>
                : <></>
            }
        </StyledAuthPageWrapper>    
    );
}

export default AuthPage;