import AuthPage from './pages/AuthPage/AuthPage';
import { MainView } from './components/style';
import { 
  useFonts, 
  Urbanist_400Regular, 
  Urbanist_500Medium, 
  Urbanist_600SemiBold, 
  Urbanist_700Bold, 
  Urbanist_600SemiBold_Italic,
  Urbanist_700Bold_Italic
} from '@expo-google-fonts/urbanist'

export default function App() {

  let [fontsLoaded] = useFonts({
    Urbanist_400Regular,
    Urbanist_500Medium,
    Urbanist_600SemiBold,
    Urbanist_700Bold,
    Urbanist_600SemiBold_Italic,
    Urbanist_700Bold_Italic
  });

  if (!fontsLoaded) {
    return null;
  }

  return (
    <MainView>
      <AuthPage/>
    </MainView>
    );
}