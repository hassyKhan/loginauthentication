document.getElementById('loginForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    const loginData = {
        username: username,
        password: password
    };

    try {
        const response = await fetch('https://localhost:7012/api/Login', {  
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        });

        if (response.ok) {
            const data = await response.json();
            document.getElementById('responseMessage').textContent = 'Login Successful! Token: ' + data.token;
            console.log('JWT Token:', data.token);
            localStorage.setItem('jwtToken', data.token);
        } else {
            document.getElementById('responseMessage').textContent = 'Login Failed! Please check your username or password.';
        }
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('responseMessage').textContent = 'An error occurred. Please try again.';
    }
});
