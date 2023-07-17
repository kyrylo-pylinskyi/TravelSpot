import { useState } from 'react';
import Greeting from '../../components/Greeting/Greeting';

import { StyledGreetingPageWrapper } from '../../components/Greeting/styledGreeting';

const GreetingPage = ({setCurrentPage}) => {

    return(
        <StyledGreetingPageWrapper>
            <Greeting setCurrentPage={setCurrentPage} />
        </StyledGreetingPageWrapper>    
    );
}

export default GreetingPage;