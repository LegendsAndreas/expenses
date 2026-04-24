window.exportExpenses = () => {
    const expenses = localStorage.getItem("expenses");
    if (!expenses) {
        alert("No expenses found in local storage");
        return;
    }

    const blob = new Blob([expenses], { type: "application/json" });
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