import { StyledTopbar } from "./styledTopbar";
import StyledBtn from "../Buttons/CustomButton";

const StyledTopBar = ({backBtn, menuBtn, backAction, menuAction}) => {
    return(
        <StyledTopbar>
            {
                backBtn && 
                <StyledBtn
                    icon='chevron-back'
                    type='nav'
                    action={() => backAction()}
                />
            }
            {
                menuBtn && 
                <StyledBtn
                    icon='menu'
                    type='nav'
                    action={() => menuAction()}
                />
            }
        </StyledTopbar>
    )
}

export default StyledTopBar;