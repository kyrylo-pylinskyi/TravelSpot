import { StyledInput } from "../elements/inputs";
import { StyledTitle,StyledText } from "../elements/typography";
import StyledBtn from "../elements/Buttons/CustomButton";

const PasswordRecovery = ({setAuthAction}) => {
    return(
        <>
            <StyledTitle>
                Forgot password?
            </StyledTitle>
            <StyledText>
                Don't worry! It occurs. Please enter the email address linked with your account.
            </StyledText>
            <StyledInput
                placeholder='Enter your email'
            />
            <StyledBtn 
                title="Send email" 
                // action={() => submitData()}
                type="primary"
            />
            <StyledBtn 
                p='15px 0 0 0'
                color='#518ef8'
                halign='center'
                title='Remember password? Login' 
                action={() => setAuthAction('login')}
                type="text"
            />
        </>
    )
}

export default PasswordRecovery;