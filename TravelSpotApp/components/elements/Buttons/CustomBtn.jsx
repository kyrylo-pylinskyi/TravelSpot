import { ButtonWrapper, ButtonText } from './styledButton';
import Icon from 'react-native-vector-icons/AntDesign';

const CustomBtn = ({title, action, type, icon, halign, p, iconColor, disabled}) => {
    return(
        <ButtonWrapper
            p={p}
            halign={halign}
            onPress={action}
            type={type}
            icon={icon}
            disabled={disabled ? disabled : false}
        >
            {title &&
                <ButtonText
                    type={type}
                >
                    {title}
                </ButtonText> 
            }
            {
                icon && <Icon name={icon} size={24} color={iconColor}/>
            }
        </ButtonWrapper>
    )
}

export default CustomBtn;