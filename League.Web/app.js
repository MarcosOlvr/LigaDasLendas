const MyApp = {
    data() {
        return {
            name: "",
            champion: {},
            champions: [],
        };
    },
    mounted() {
        fetch("https://localhost:7251/api/champ/all")
        .then((response) => response.json())
        .then((data) => (this.champions = data))
    },
    methods: {
        getChampByName() {
            fetch(`https://localhost:7251/api/champ/${this.name}`)
            .then((response) => response.json())
            .then((data) => (this.champion = data));
        },
    },
};

Vue.createApp(MyApp).mount('#app');