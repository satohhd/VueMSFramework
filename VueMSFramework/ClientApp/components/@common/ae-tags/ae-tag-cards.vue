<template>
    <b-card-group v-else-if="field.tag === 'cards'" columns>
        <b-card v-for="v in viewModel[field.name]"
                :key="v.title"
                :title="v.title"
                :class="field.name"
                :img-src="v.icon"
                img-alt="image"
                :img-fluid=false
                img-top>
                 <p class="card-text">
                <span v-html="v.description"></span>
                <br /><span style="font-size:0.8em" v-if="v.lastModified">&#9654;最終更新&nbsp;:&nbsp;{{ $moment(v.lastModified).fromNow() }}</span>
            </p>
            <b-button @click.stop.prevent="listener(v)" variant="success">詳細はこちら&nbsp;&#8811;</b-button>
        </b-card>
    </b-card-group>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

        },
 
    }

</script>
<style scoped>
</style>
