window.exportExpenses = () => {
    const expenses = localStorage.getItem("expenses");
    if (!expenses) {
        alert("No expenses found in local storage");
        return;
    }

    const blob = new Blob([expenses], {type: "application/json"});
    const url = URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = "expenses.json";
    a.click();

    URL.revokeObjectURL(url);
};

window.importExpenses = () => {
    console.log("Importing expenses...");
    const fileInput = document.createElement("input");
    fileInput.type = "file";
    fileInput.accept = ".json";

    fileInput.onchange = async (event) => {
        const file = event.target.files[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = async (e) => {
            const expenses = e.target.result;
            localStorage.setItem("expenses", expenses);
            alert("Expenses imported successfully");
        };
        reader.readAsText(file);
    };

    fileInput.click();
};

window.shakeElement = function (iconNameId) {
    const element = document.getElementById(iconNameId);
    if (!element) {
        console.error(`Element with ID ${iconNameId} not found`);
        throw new Error(`Element with ID ${iconNameId} not found`);
    }

    element.classList.remove("shake");
    void element.offsetWidth; // force reflow to restart animation
    element.classList.add("shake");
    element.addEventListener("animationend", () => element.classList.remove("shake"), {once: true});
}

window.setHeight = function () {
    const setHeight = document.querySelectorAll(".js-set-height");
    setHeight.forEach(element => {
        console.log(`[${new Date().toISOString()}] Height: ` + element.style.height);
        console.log(`[${new Date().toISOString()}] Scroll Height: ` + element.scrollHeight);
        element.style.height = `${element.scrollHeight}px`;
    });
};

window.removeExpenseButton = function (itemId, containerId) {
    const expense = document.querySelector("#" + itemId);
    if (!expense) {
        console.error(`[${new Date().toISOString()}] Expense with ID ${itemId} not found`);
        return;
    }

    if (expense) {
        const style = getComputedStyle(expense);
        const marginTop = parseFloat(style.marginTop);
        const marginBottom = parseFloat(style.marginBottom);
        const expenseTotalHeight = expense.offsetHeight + marginTop + marginBottom;
        const setHeight = document.querySelector("#" + containerId);
        const setHeightHeight = setHeight.offsetHeight;
        setHeight.style.height = `${setHeightHeight - expenseTotalHeight}px`;
    }
};

window.adjustInputSize = function (id) {
    console.log("adjustInputSize " + id);
    const input = document.querySelector("#expense-" + id);
    const dummy = document.querySelector("#span-" + id);

    console.log(dummy);
    dummy.textContent = input.value || ' ';
    input.style.setProperty('width', dummy.offsetWidth + 'px', 'important');
}

window.initAdjustInputSize = function () {
    console.log("initAdjustInputSize");
    const inputWrappers = document.querySelectorAll(".js-init-adjust-input-size");

    inputWrappers.forEach(wrapper => {
        const input = wrapper.querySelector(".js-init-adjust-input-size-input");
        const span = wrapper.querySelector(".js-init-adjust-input-size-span");

        span.textContent = input.value || ' ';
        input.style.setProperty('width', span.offsetWidth + 'px', 'important');
    })

}

window.initAddExpensesSummaryPopupOnHoverListeners = function () {
    const expensesSummaryPopups = document.querySelectorAll(".js-toggle-expenses-popup");
    if (expensesSummaryPopups.length === 0) {
        console.error("No expenses summary popup found");
    } else {
        console.log("expensesSummaryPopup.length: " + expensesSummaryPopups.length);
    }
    expensesSummaryPopups.forEach(expensesSummaryPopup => {
        console.log("initAddExpensesSummaryPopupOnHoverListeners");
        let popup = expensesSummaryPopup.querySelector(".expenses-summary-popup");
        popup.addEventListener("mouseover", () => popup.classList.add("expenses-summary-popup__show-popup"));
        popup.addEventListener("mouseleave", () => popup.classList.remove("expenses-summary-popup__show-popup"));
    });
}

window.showMe = function (event) {
    if (!event) {
        console.log("target not found");
        return;
    }
    let popup = event.querySelector(".expenses-summary-popup");
    if (!popup) {
        console.log("popup not found");
        return;
    }
    popup.classList.add("show-popup");
}

window.addHoverEvents = function () {
    const expensesSummaries = document.querySelectorAll(".expenses-summary");
    expensesSummaries.forEach(summary => {
        const popup = summary.querySelector(".expenses-summary__popup");
        if (popup) {
            summary.addEventListener("mouseenter", () => {
                const popupBounds = popup.getBoundingClientRect();
                const windowBounds = document.documentElement.getBoundingClientRect();
                console.log("popup: ", popupBounds);
                console.log("window: ", windowBounds);
            });
            summary.addEventListener("mouseleave", () => {

            });
        } else {
            console.log("popup not found");
        }
    })
}

window.adjustOutBoundSummaryPopups = function () {
    const expensesSummaryPopups = document.querySelectorAll(".expenses-summary-popup");
    expensesSummaryPopups.forEach(popup => {
        const popupBounds = popup.getBoundingClientRect();
        const windowBounds = document.documentElement.getBoundingClientRect();
        const isPopupOutsideWindow = popupBounds.bottom > windowBounds.bottom;
        if (isPopupOutsideWindow) {
            popup.classList.add("expenses-summary-popup__show-popup");
        }
    })
}