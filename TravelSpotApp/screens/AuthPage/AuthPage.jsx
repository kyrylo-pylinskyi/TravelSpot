import { useState } from 'react';
import { Text } from 'react-native';
import Login from '../../components/Login/Login';
import Registration from '../../components/Login/Registration';
import PasswordRecovery from '../../components/Login/PasswordRecovery';

import { StyledContainer } from '../../components/elements/styledContainer';
import { StyledAuthPageWrapper } from '../../components/Login/styledAuth';
import StyledTopBar from '../../components/elements/Topbar/Topbar';

const AuthPage = ({basicAction, setCurrentPage}) => {

    const [authAction, setAuthAction] = useState(basicAction !== '' ? basicAction : 'login')

    return(
        <StyledAuthPageWrapper>
            <StyledTopBar 
                backBtn 
                backAction={() => setCurrentPage('greeting')}
            />
            <StyledContainer>
                {
                    authAction === 'login' ? <Login setAuthAction={setAuthAction} setCurrentPage={setCurrentPage}/> 
                    : authAction === 'registration' ? <Registration setAuthAction={setAuthAction} setCurrentPage={setCurrentPage}/>
                    : authAction === 'psswdConfirmation' ? <Text>Confirm password</Text>
                    : authAction === 'psswdRecovery' ? <PasswordRecovery setAuthAction={setAuthAction}/>
                    : <></>
                }
            </StyledContainer>
        </StyledAuthPageWrapper>    
    );
}

export default AuthPage;