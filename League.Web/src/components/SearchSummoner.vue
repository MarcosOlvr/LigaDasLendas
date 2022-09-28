<script lang="ts">
    import { defineComponent, onMounted, ref } from "vue";
    import api from "../services/api";  
    
    export default defineComponent({
        name: "SearchSummoner",
        setup() {
            const summoner = {};
            const summonerName = "";

            return { summoner, summonerName };
        },
        methods: {
            search() {
                api.get(`/summoner/${this.summonerName}`)
                .then((response) => (this.summoner = response.data));
            },
        },
    });
    </script>
    
    <template>
        <main>
            <div>
                <div id="form">
                    <input type="text" v-model="summonerName">
                    <button v-on:click="search">Buscar</button>
                </div>
                <p>{{ summoner.name }}</p>
                <p>{{ summoner.profileIconId }}</p>
            </div>
        </main>
    </template>