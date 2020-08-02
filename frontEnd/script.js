const apiEndpoint = "https://localhost:5001";


const creditCardFrom = document.getElementById('put-from');
const amountFrom = document.getElementById('put-amount');
const putMoneyBtn = document.getElementById('put-money');


const creditCardTo = document.getElementById('transfer-to');
const amountTo = document.getElementById('transfer-amount');
const transferMoneyBtn = document.getElementById('transfer-money');

const main = document.getElementById('main');

const current_error = document.getElementById('current-error');



async function getAccountTransactions(){
    var resp = await fetch(`${apiEndpoint}/Account/transactions`);
    var data = await resp.json();
    return data;
}

async function getCurrentAmount() {
    var resp = await fetch(`${apiEndpoint}/Account/currentAmount`);
    var data = await resp.json();
    return data;
}

async function putMoney(accountFrom, amount) {

    var request = {
        creditCardFrom: accountFrom,
        amount: amount
    }

    const response = await fetch(`${apiEndpoint}/Account/putMoney`, {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    });
}

async function transferMoney(accountTo, amount) {

    var request = {
        creditCardTo: accountTo,
        amount: amount
    }

    const response = await fetch(`${apiEndpoint}/Account/transferMoney`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    });

    if(response.status !== 200){
        throw 'Not enough money';
    }
}

async function updateUI() {

    var currentAmout = await getCurrentAmount();
    var transactionsLog = await getAccountTransactions();

    main.innerHTML ='<h2><strong>Account</strong> Status</h2>';

    console.log(transactionsLog);

    transactionsLog.forEach((item) => {
        const element = document.createElement('div');
        element.classList.add('transaction');
        element.innerHTML = `<strong>${item.creditCard}</strong> ${formatMoney(item.amount)}`;
        main.appendChild(element);
    });

    const currentAmount = document.createElement('div');
    currentAmount.innerHTML = `<h3>Current Amount: <strong>${formatMoney(currentAmout)}</strong></h3>`;
    main.appendChild(currentAmount);
}

// Format money
function formatMoney(number) {
    return '$' + number.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}


function showError()
{
    current_error.classList.remove('hide');
}

function hideError()
{
    current_error.classList.add('hide');
}

putMoneyBtn.addEventListener('click', async function (evt) {
    evt.preventDefault();
    hideError();

    var accountFrom = creditCardFrom.value;
    var amount = Number.parseFloat(amountFrom.value);

    await putMoney(accountFrom, amount);

    updateUI();
});

transferMoneyBtn.addEventListener('click', async function (evt) {
    evt.preventDefault();
    hideError();

    var accountTo = creditCardTo.value;
    var amount = Number.parseFloat(amountTo.value);

    console.log(amountTo.value);

    try {
        await transferMoney(accountTo, amount);
    }
    catch {
        showError();
    }
    
    updateUI();
});


updateUI();