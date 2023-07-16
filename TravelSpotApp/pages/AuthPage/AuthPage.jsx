import { useState } from 'react';

import Login from '../../components/Login/Login';
import Registration from '../../components/Login/Registration';
import { StyledAuthPageWrapper } from '../../components/Login/styledAuth';

const AuthPage = () => {

    const [authAction, setAuthAction] = useState('test')

    return(
        <StyledAuthPageWrapper>
            {
                authAction === 'login' 
                    ? <Login setAuthAction={setAuthAction}/> 
                    : <Registration setAuthAction={setAuthAction}/>
            }
        </StyledAuthPageWrapper>    
    );
}

export default AuthPage;