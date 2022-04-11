getAnswer = (req, res) => {
    const data = {
        answer: 2
    }

    return res.status(200).json({ success: true, data: data })
};

module.exports = {
    getAnswer
};