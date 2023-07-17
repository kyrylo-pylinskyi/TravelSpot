import { useState } from 'react';
import { Text } from 'react-native';

import Login from '../../components/Login/Login';
import Registration from '../../components/Login/Registration';
import { StyledAuthPageWrapper } from '../../components/Login/styledAuth';
import PasswordRecovery from '../../components/Login/PasswordRecovery';

const AuthPage = () => {

    const [authAction, setAuthAction] = useState('login')

    return(
        <StyledAuthPageWrapper>
            {
                authAction === 'login' ? <Login setAuthAction={setAuthAction}/> 
                : authAction === 'registration' ? <Registration setAuthAction={setAuthAction}/>
                : authAction === 'psswdConfirmation' ? <Text>Confirm password</Text>
                : authAction === 'psswdRecovery' ? <PasswordRecovery setAuthAction={setAuthAction}/>
                : <></>
            }
        </StyledAuthPageWrapper>    
    );
}

export default AuthPage;