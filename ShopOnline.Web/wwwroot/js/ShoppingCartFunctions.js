function MakeUpdateQtyButtonVisible(id, visible) {
    const updateQtyButton = document.querySelector("button[data-itemId='" + id + "']");

    if (visible) {
        updateQtyButton.style.display = "inline-block";
    }
    else {
        updateQtyButton.style.display = "none";
    }
}