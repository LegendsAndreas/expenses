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
    const input = document.querySelector("#expenses-" + id);
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

window.initSummaryPopupClamping = () => {
    if (window._clampInit) return;
    window._clampInit = true;

    const clampPopup = (summary) => {
        const popup = summary.querySelector('.expenses-summary__popup');
        if (!popup) return;

        popup.style.transform = '';
        const rect = popup.getBoundingClientRect();
        if (rect.width === 0) return; // not visible

        const margin = 20;
        const vw = document.documentElement.clientWidth; // more reliable than innerWidth on mobile
        const vh = window.visualViewport ? window.visualViewport.height : window.innerHeight;

        let shiftX = 0;
        if (rect.right > vw - margin) {
            shiftX = (vw - margin) - rect.right;
        } else if (rect.left < margin) {
            shiftX = margin - rect.left;
        }

        let shiftY = 0;
        if (rect.bottom > vh - margin) {
            shiftY = (vh - margin) - rect.bottom;
        }

        popup.style.transform = `translate(${shiftX}px, ${shiftY}px)`;
    };

    const handler = (e) => {
        const summary = e.target.closest('.expenses-summary');
        if (!summary) return;
        // Wait a frame so :hover / :focus / :active / class changes are applied before measuring
        requestAnimationFrame(() => clampPopup(summary));
    };

    ['pointerover', 'pointerdown', 'touchstart', 'focusin', 'click'].forEach((type) =>
        document.addEventListener(type, handler, { passive: true })
    );

    // Startup run, after layout is ready
    const initial = () =>
        document.querySelectorAll('.expenses-summary').forEach(clampPopup);

    if (document.readyState === 'complete') {
        requestAnimationFrame(initial);
    } else {
        window.addEventListener('load', () => requestAnimationFrame(initial), { once: true });
    }
};

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