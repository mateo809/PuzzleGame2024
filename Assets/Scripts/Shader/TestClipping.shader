Shader "Unlit/TestClipping"
{
    SubShader
    {
        Tags{"Queue" = "Transparent+1"}

        Pass 
        {
            Blend Zero One
        }
    }
}
